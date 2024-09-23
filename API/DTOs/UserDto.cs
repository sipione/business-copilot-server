public class UpdateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public AccountStatus? Status { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    public string? CurrentProfilePicturePath { get; set; }
}

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    public AccountStatus? Status { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
