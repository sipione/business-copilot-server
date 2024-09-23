using APP.Enums;
namespace APP.Entities;
public class SubAccounts{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid StakeholderId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public AccountStatus Status { get; set; }
    public SubAccountRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Progress> Progresses { get; set; }
    public List<Permitions> PermitionsList { get; set; }

    public SubAccounts(Guid userId, Guid stakeholderId, string name, string? description, string email, string password, SubAccountRole role){
        Id = Guid.NewGuid();
        UserId = userId;
        StakeholderId = stakeholderId;
        Name = name;
        Description = description;
        Email = email;
        Password = password;
        Status = AccountStatus.ACTIVE;
        Role = role;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Progresses = new List<Progress>();
        PermitionsList = new List<Permitions>();
        this.AllowPermitionsByRole();
    }

    public SubAccounts() { }

    public void AllowPermitionsByRole(){
        List<Permitions> allPermitions = Permitions.GetValues(typeof(Permitions)).Cast<Permitions>().ToList();

        if(SubAccountRole.EDITOR == this.Role)
        {
            this.PermitionsList = allPermitions.Where(p => !p.ToString().Contains("USERS") && !p.ToString().Contains("DELETE") && !p.ToString().Contains("CREATE")).ToList();
        }
        else if(SubAccountRole.VIWER == this.Role)
        {
            this.PermitionsList = allPermitions.Where(p => !p.ToString().Contains("USERS") && p.ToString().Contains("READ")).ToList();
        }else{
            this.PermitionsList = new List<Permitions>();
        }
    }

    public override string ToString(){
        return $"Id: {Id}, User: {UserId}, StakeholderId: {StakeholderId}, Name: {Name}, Description: {Description}, Email: {Email}, Password: {Password}, Status: {Status}, Role: {Role}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, Progresses: {Progresses}, PermitionsList: {PermitionsList}";
    }
}