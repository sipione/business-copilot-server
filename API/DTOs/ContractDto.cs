public class CreateContractDto{
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
}

public class UpdateContractDto{
    public Guid Id { get; set; }
    public Guid? StakeholderId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public float? InitialAmount { get; set; }
    public float? Discount { get; set; }
    public int? Installments { get; set; }
    public float? Interest { get; set; }
    public float? Penalty { get; set; }
    public float? TotalAmount { get; set; }
    public float? PaidAmount { get; set; }
    public float? RemainingAmount { get; set; }
    public string? DocumentPath { get; set; }
    public Guid? VoucherId { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
    public ContractStatus? ContractStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}