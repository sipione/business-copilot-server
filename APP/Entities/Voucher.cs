using System.Text.Json.Serialization;
using APP.Enums;

namespace APP.Entities;
public class Voucher
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? StackholderId { get; set; }
    public string Code { get; set; }
    public decimal Value { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; }
    public AccountStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [JsonIgnore]
    public ICollection<Contract> Contracts { get; set; }
    [JsonIgnore]
    public User OwnerData { get; set; }

    public Voucher(Guid userId, Guid? stackholderId, string code, decimal value, DateTime? expirationDate, int? usageLimit, int usageCount, AccountStatus status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        StackholderId = stackholderId;
        Code = code;
        Value = value;
        ExpirationDate = expirationDate;
        UsageLimit = usageLimit;
        UsageCount = usageCount;
        Status = status;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Voucher() { }

    public void UseVoucher(){
        if (UsageCount < UsageLimit)
        {
            UsageCount++;
        }
        else
        {
            Status = AccountStatus.CANCELED;
        }
    }

    public override string ToString(){
        return $"Id: {Id}, UserId: {UserId}, StackholderId: {StackholderId}, Code: {Code}, Value: {Value}, ExpirationDate: {ExpirationDate}, UsageLimit: {UsageLimit}, UsageCount: {UsageCount}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}