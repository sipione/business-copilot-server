public class UpdateUserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserRole? Role { get; set; }
    public string? ProfilePicture { get; set; }
}

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string? ProfilePicture { get; set; }
}
