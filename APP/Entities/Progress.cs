public class Progress
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public SubAccounts SubAccount { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string>? FilesPaths { get; set; }

    public Progress(User user, SubAccounts subAccount, string title, string? description)
    {
        Id = Guid.NewGuid();
        User = user;
        SubAccount = subAccount;
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        FilesPaths = new List<string>();
    }

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Description: {Description}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}