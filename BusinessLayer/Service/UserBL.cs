using BusinessLayer.Interface;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
        }

        public bool RegisterUser(UserEntity model)
        {
            logger.Info("UserBL: RegisterUser method called.");
            return _userRL.RegisterUser(model);
        }

        public bool LoginUser(LoginModel model)
        {
            logger.Info("UserBL: LoginUser method called.");
            return _userRL.LoginUserRL(model);
        }

        public UserEntity GetUserByUsername(string userName)
        {
            logger.Info("UserBL: GetUserByUsername method called.");
            return _userRL.GetUserByUsername(userName);
        }
        public bool ValidateEmail(string email)
        {
            return _userRL.ValidateEmail(email);
        }
        public bool ResetPassword(string email, string newPassword)
        {
            logger.Info("UserBL: ResetPassword method called.");
            return _userRL.ResetPassword(email, newPassword);
        }
    }
}
