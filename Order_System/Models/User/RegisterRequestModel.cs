namespace Order_System.Models.User;

public class RegisterRequestModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNo { get; set; } = null!;
    public string Password { get; set; } = null!;
}