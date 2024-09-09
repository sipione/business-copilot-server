namespace APP.Entities;
public class Voucher
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public Stakeholder? Stackholder { get; set; }
    public string Code { get; set; }
    public decimal Value { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; }
    public AccountStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Contract> Contracts { get; set; }

    public Voucher(User user, Stakeholder stackholder, string code, decimal value, DateTime expirationDate, int usageLimit)
    {
        User = user;
        Stackholder = stackholder;
        Code = code;
        Value = value;
        ExpirationDate = expirationDate;
        UsageLimit = usageLimit;
        UsageCount = 0;
        Status = AccountStatus.ACTIVE;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Contracts = new List<Contract>();
    }

    public Voucher() { }

    public void UseVoucher()
    {
        if (UsageCount < UsageLimit)
        {
            UsageCount++;
        }
        else
        {
            Status = AccountStatus.CANCELED;
        }
    }

    public override string ToString()
    {
        return $"Id: {Id}, Code: {Code}, Value: {Value}, ExpirationDate: {ExpirationDate}, UsageLimit: {UsageLimit}, UsageCount: {UsageCount}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}