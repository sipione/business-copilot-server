using APP.Enums;

public class CreateSubAccountDto
{
    public Guid UserId { get; set; }
    public Guid StakeholderId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public AccountStatus Status { get; set; }
    public SubAccountRole Role { get; set; }
}

public class UpdateSubAccountDto
{
    public Guid Id { get; set; }
    public Guid? StakeholderId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public AccountStatus? Status { get; set; }
    public SubAccountRole? Role { get; set; }
}