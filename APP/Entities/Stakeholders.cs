using Microsoft.VisualBasic;

namespace APP.Entities;
public class Stakeholder{
    public Guid Id { get; set; }
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
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual ICollection<IncomeCashFlow> IncomeCashFlows { get; set; }
    public virtual ICollection<ExpenseCashFlow> ExpenseCashFlows { get; set; }
    public virtual ICollection<Voucher> Vouchers { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }


    public Stakeholder(Guid userId, string name, string email, string? phone, string? address, string? city, string? state, string? country, string? zipCode, StakeholderType type, AccountStatus status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Type = type;
        Status = status;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Stakeholder() { }

    public override string ToString(){
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}, Address: {Address}, City: {City}, State: {State}, Country: {Country}, ZipCode: {ZipCode}, Type: {Type}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}