namespace ModelLayer.Model
{
    // Model for registration
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    // Model for login
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    // Model for reset password request
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
}
