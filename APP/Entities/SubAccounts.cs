public class SubAccounts
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public Stakeholder Stakeholder { get; set; }
    public string Name => Stakeholder.Name;
    public string? Description { get; set; }
    public string Email => Stakeholder.Email;
    public string Password { get; set; }
    public AccountStatus Status { get; set; }
    public SubAccountRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Progress> Progresses { get; set; }
    public List<Permitions> Permitions { get; set; }

    public SubAccounts(User user, Stakeholder stakeholder, string? description, string password, SubAccountRole role)
    {
        Id = Guid.NewGuid();
        User = user;
        Stakeholder = stakeholder;
        Description = description;
        Password = password;
        Status = AccountStatus.ACTIVE;
        Role = role;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Progresses = new List<Progress>();
        Permitions = new List<Permitions>();
    }

    public string toString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Role: {Role}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}