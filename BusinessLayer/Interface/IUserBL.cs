using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public bool RegisterUser(UserEntity model);
        public bool LoginUser(LoginModel model);
        public UserEntity GetUserByUsername(string userName);
        public bool ValidateEmail(string email);
        public bool ResetPassword(string email, string newPassword);
    }
}
