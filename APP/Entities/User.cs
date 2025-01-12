using APP.Enums;
using System.Text.Json.Serialization;

namespace APP.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public AccountStatus Status { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
    public virtual ICollection<IncomeCashFlow> IncomeCashFlows { get; set; }
    public virtual ICollection<ExpenseCashFlow> ExpenseCashFlows { get; set; }
    public virtual ICollection<SubAccounts> SubAccounts { get; set; }
    public virtual ICollection<Stakeholder> Stakeholders { get; set; }
    public virtual ICollection<Voucher> Vouchers { get; set; }
    public virtual ICollection<Progress> Progresses { get; set; }
    public List<Permitions> UserPermitionsList { get; set; }

    public User(string name, string email, string password, UserRole role, string? profilePicture, AccountStatus? accountStatus){
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        Status = accountStatus ?? AccountStatus.ACTIVE;
        ProfilePicture = profilePicture;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Contracts = new List<Contract>();
        IncomeCashFlows = new List<IncomeCashFlow>();
        ExpenseCashFlows = new List<ExpenseCashFlow>();
        SubAccounts = new List<SubAccounts>();
        Stakeholders = new List<Stakeholder>();
        Vouchers = new List<Voucher>();
        Progresses = new List<Progress>();
        UserPermitionsList = new List<Permitions>();
        this.AllowPermitionsByRole();
    }

    public User() {
        UserPermitionsList = new List<Permitions>();
    }

    public void AllowPermitionsByRole(){
        List<Permitions> allPermitions = Permitions.GetValues(typeof(Permitions)).Cast<Permitions>().ToList();

        if(UserRole.ADMIN == this.Role){
            this.UserPermitionsList = allPermitions;
        }else if(UserRole.ORDINARY == this.Role || UserRole.VIP == this.Role){
            this.UserPermitionsList = allPermitions.Where(p => !p.ToString().Contains("USERS")).ToList();
        }else if(UserRole.EDITOR == this.Role){
            this.UserPermitionsList = allPermitions.Where(p => !p.ToString().Contains("USERS") && !p.ToString().Contains("DELETE") && !p.ToString().Contains("CREATE")).ToList();
        }else if(UserRole.GUEST == this.Role){
            this.UserPermitionsList = allPermitions.Where(p => !p.ToString().Contains("USERS") && p.ToString().Contains("READ")).ToList();
        }
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Role: {Role}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}