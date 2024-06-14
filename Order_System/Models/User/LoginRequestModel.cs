    namespace Order_System.Models.User;

    public class LoginRequestModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }