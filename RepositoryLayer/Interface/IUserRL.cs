using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public bool RegisterUser(UserEntity user);
        public bool LoginUserRL(LoginModel loginModel);
        public UserEntity GetUserByUsername(string userName);
        public bool ResetPassword(string email, string newPassword);
    }
}
