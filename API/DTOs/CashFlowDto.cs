using APP.Entities;
using APP.Enums;

public class CreateIncomeCashFlowDto{
    public Guid? ContractId { get; set; }
    public required string Description { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentStatus Status { get; set; }
    public IncomeCategory Category { get; set; }
}

public class UpdateIncomeCashFlowDto{
    public Guid Id { get; set; }
    public Guid? ContractId { get; set; }
    public required string Description { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentStatus Status { get; set; }
    public IncomeCategory Category { get; set; } 
}

public class CreateExpenseCashFlowDto{
    public Guid? ContractId { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentStatus Status { get; set; }
    public ExpensesCategory Category { get; set; }
}

public class UpdateExpenseCashFlowDto{
    public Guid Id { get; set; }
    public Guid? ContractId { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentStatus Status { get; set; }
    public ExpensesCategory Category { get; set; }
}