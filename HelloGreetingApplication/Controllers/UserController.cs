using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;

namespace HelloGreetingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TokenService _jwtService;

        public UserController(IUserBL userBL, TokenService jwtService)
        {
            _userBL = userBL;
            _jwtService = jwtService;
        }

        /// <summary>
        /// API to register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserEntity model)
        {
            logger.Info($"Register request received for {model.UserName}");
            bool result = _userBL.RegisterUser(model);
            if (result)
            {
                logger.Info($"User registered successfully for {model.UserName}");
                return Ok("User registered successfully");
            }
            logger.Error("Error occurred during registration");
            return BadRequest("Error occurred during registration");
        }

        /// <summary>
        /// API for user login
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            logger.Info($"Login request received for {model.UserName}");
            var result = _userBL.LoginUser(model);
            var message = "Login Successful";
            if (!result)
            {
                logger.Error("Invalid credentials");
                return Unauthorized(new { message = "Invalid credentials" });
            }

            logger.Info($"User {model.UserName} logged in successfully");
            var tokenService = HttpContext.RequestServices.GetRequiredService<TokenService>();
            string token = tokenService.GenerateToken(model.UserName);

            return Ok(new {Message = message, Token = token});
        }

        /// <summary>
        /// API to send email for resetting password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel request)
        {
            try
            {
                // Validate the email
                if (string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Email is required.");
                }

                bool validEmail = _userBL.ValidateEmail(request.Email);
                if (!validEmail)
                {
                    return BadRequest("Invalid email.");
                }

                // Generate JWT reset token
                var resetToken = _jwtService.GenerateResetToken(request.Email);

                // Create the email payload
                var message = new
                {
                    To = request.Email,
                    Subject = "Reset Your Password",
                    Body = $"Click the link to reset your password: https://yourdomain.com/reset-password?token={resetToken}"
                };

                return Ok("Password reset email has been sent.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error occurred while processing the password reset", error = ex.Message });
            }
        }

        /// <summary>
        /// API to reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            logger.Info($"ResetPassword request received for {model.Email}");
            if (model.NewPassword == model.ConfirmPassword)
            {
                bool result = _userBL.ResetPassword(model.Email, model.NewPassword);
                if (result)
                {
                    return Ok("Password reset successful");
                }
                return BadRequest("Error occurred during password reset");
            }
            return BadRequest("Passwords do not match");
        }

    }
}
