public class CreateStakeholderDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public StakeholderType Type { get; set; }
    public AccountStatus Status { get; set; }
}

public class UpdateStakeholderDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public StakeholderType? Type { get; set; }
    public AccountStatus? Status { get; set; }
}