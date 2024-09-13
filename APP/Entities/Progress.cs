namespace APP.Entities;
public class Progress
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SubAccountId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string>? FilesPaths { get; set; }

    public Progress(User user, SubAccounts subAccount, string title, string? description){
        Id = Guid.NewGuid();
        UserId = user.Id;
        SubAccountId = subAccount.Id;
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Progress() { }

    public override string ToString(){
        return $"Id: {Id}, Title: {Title}, Description: {Description}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}