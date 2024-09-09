namespace APP.Entities;
public class Stakeholder{
    public Guid Id { get; set; }
    public User User { get; set; }
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
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<IncomeCashFlow> IncomeCashFlows { get; set; }
    public List<ExpenseCashFlow> ExpenseCashFlows { get; set; }
    public List<Voucher> Vouchers { get; set; }
    public List<Contract> Contracts { get; set; }

    public Stakeholder(User user, string name, string email, StakeholderType type, string? phone, string? address, string? city, string? state, string? country, string? zipCode)
    {
        Id = Guid.NewGuid();
        User = user;
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Type = type;
        Status = AccountStatus.ACTIVE;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        IncomeCashFlows = new List<IncomeCashFlow>();
        ExpenseCashFlows = new List<ExpenseCashFlow>();
        Vouchers = new List<Voucher>();
        Contracts = new List<Contract>();
    }

    public Stakeholder() { }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Type: {Type}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}