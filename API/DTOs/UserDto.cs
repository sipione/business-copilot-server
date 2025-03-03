using APP.Entities;
using APP.Enums;

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
public class RegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}


public class UserResponseDto
{
    public String Name { get; set; }
    public String Email { get; set; }
    public UserRole Role { get; set; }
    public AccountStatus Status { get; set; }
    public String? ProfilePicture { get; set; }
    public List<StakeholderRefDto>? Stakeholders { get; set; }
}