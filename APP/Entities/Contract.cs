namespace APP.Entities;

public class Contract
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid StakeholderId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public float InitialAmount { get; set; }
    public float? Discount { get; set; }
    public int? Installments { get; set; }
    public float? Interest { get; set; }
    public float? Penalty { get; set; }
    public float? TotalAmount { get; set; }
    public float? PaidAmount { get; set; }
    public float? RemainingAmount { get; set; }
    public string? DocumentPath { get; set; }
    public Guid? VoucherId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public ContractStatus ContractStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Contract(Guid userId, Guid stakeholderId, string title, string description, float initialAmount, float? discount, int? installments, float? interest, float? penalty, float? totalAmount, float? paidAmount, float? remainingAmount, string documentPath, Guid? voucherId, PaymentStatus paymentStatus, ContractStatus contractStatus, DateTime startDate, DateTime? endDate)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        StakeholderId = stakeholderId;
        Title = title;
        Description = description;
        InitialAmount = initialAmount;
        Discount = discount;
        Installments = installments;
        Interest = interest;
        Penalty = penalty;
        TotalAmount = totalAmount;
        PaidAmount = paidAmount;
        RemainingAmount = remainingAmount;
        DocumentPath = documentPath;
        VoucherId = voucherId;
        PaymentStatus = paymentStatus;
        ContractStatus = contractStatus;
        StartDate = startDate;
        EndDate = endDate;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Contract() { }

    public override string ToString(){
        return $"Id: {Id}, Title: {Title}, Description: {Description}, InitialAmount: {InitialAmount}, Discount: {Discount}, Installments: {Installments}, Interest: {Interest}, Penalty: {Penalty}, TotalAmount: {TotalAmount}, PaidAmount: {PaidAmount}, RemainingAmount: {RemainingAmount}, DocumentPath: {DocumentPath}, PaymentStatus: {PaymentStatus}, ContractStatus: {ContractStatus}, StartDate: {StartDate}, EndDate: {EndDate}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}