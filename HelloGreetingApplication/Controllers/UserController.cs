using BusinessLayer.Interface;
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

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
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
            var message = "Login Successful";
            if (_userBL.LoginUser(model))
            {
                logger.Info($"User logged in successfully for {model.UserName}");
                return Ok(message);
            }
            logger.Warn($"Invalid credentials received for login with username: {model.UserName}");
            return Unauthorized(message);
        }

        /// <summary>
        /// API to send email for resetting password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            return BadRequest("Email sent successfully");
        }

        /// <summary>
        /// API to reset password
        /// </summary>
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            return BadRequest("Passwords do not match");
        }
    }
}
