using ModelLayer.Model;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Hashing;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly GreetingContext _context;
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly Password_Hash password_Hash = new Password_Hash();

        public UserRL(GreetingContext context)
        {
            _context = context;
        }

        public bool RegisterUser(UserEntity newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                logger.Warn("Invalid user data received for registration.");
                return false;
            }

            var userEntity = new UserEntity
            {
                Email = newUser.Email,
                UserName = newUser.UserName,
                Password = password_Hash.HashPassword(newUser.Password)
            };
            // Add the new user to the database
            _context.Users.Add(userEntity);
            _context.SaveChanges();

            logger.Info($"User registered successfully! with email: {newUser.Email}");

            return true;
        }


        public bool LoginUserRL(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                logger.Warn("Invalid user data received for login.");
                return false;
            }
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginModel.UserName);
            if (user != null && password_Hash.VerifyPassword(loginModel.Password, user.Password))
            {
                logger.Info($"User logged in successfully! with username: {loginModel.UserName}");
                return true;
            }
            logger.Warn($"Invalid credentials received for login with username: {loginModel.UserName}");
            return false;
        }

        public UserEntity GetUserByUsername(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                logger.Warn("Invalid username received for fetching user.");
                return null;
            }
            logger.Info($"Fetching user with username: {userName}");
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public bool ValidateEmail(string email)
        {
            var result = _context.Users.FirstOrDefault(user => user.Email == email);
            Console.WriteLine(result);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool ResetPassword(string email, string newPassword)
        {
            logger.Info($"Resetting password for user with email: {email}");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                logger.Info($"Password reset successfully for user with email: {email}");
                user.Password = password_Hash.HashPassword(newPassword);
                _context.SaveChanges();
                return true;
            }
            logger.Warn($"Invalid email received for password reset: {email}");
            return false;
        }
    }
}
