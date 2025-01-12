using APP.Enums;

namespace APP.Entities;

public class CashFlow
{
    public Guid Id { get; set; }
    public Guid? ContractId { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual User OwnerData { get; set; }
    public virtual Contract? Contract { get; set; }

    public CashFlow(Guid? ContractId, Guid UserId, string Description, float Amount, DateTime TransactionDate, PaymentStatus Status)
    {
        Id = Guid.NewGuid();
        this.ContractId = ContractId;
        this.UserId = UserId;
        this.Description = Description;
        this.Amount = Amount;
        this.TransactionDate = TransactionDate;
        this.Status = Status;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public CashFlow(){}

    public override string ToString()
    {
        return $"Id: {Id}, Description: {Description}, Amount: {Amount}, TransactionDate: {TransactionDate}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}


public class IncomeCashFlow : CashFlow
{
    public IncomeCategory Category { get; set; }

    public IncomeCashFlow(Guid? contractId, Guid userId, string description, float amount, DateTime transactionDate, PaymentStatus status, IncomeCategory category) : base(contractId, userId, description, amount, transactionDate, status)
    {
        Category = category;
    }

    public IncomeCashFlow(){}

    public override string ToString(){
        return $"Id: {Id}, Description: {Description}, Amount: {Amount}, TransactionDate: {TransactionDate}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, Category: {Category}";
    }

    public void UpdateFields(IncomeCashFlow newIncome){
        ContractId = newIncome.ContractId ?? this.ContractId;
        Description = newIncome.Description;
        Amount = newIncome.Amount;
        TransactionDate = newIncome.TransactionDate;
        Status = newIncome.Status;
        Category = newIncome.Category;
        UpdatedAt = DateTime.Now;
    }
}

public class ExpenseCashFlow : CashFlow
{
    public ExpensesCategory Category { get; set; }

    public ExpenseCashFlow(Guid? contractId, Guid userId, string description, float amount, DateTime transactionDate, PaymentStatus status, ExpensesCategory category) : base(contractId, userId, description, amount, transactionDate, status)
    {
        Category = category;
    }

    public ExpenseCashFlow(){}

    public void UpdateFields(ExpenseCashFlow newExpense){
        ContractId = newExpense.ContractId ?? this.ContractId;
        Description = newExpense.Description;
        Amount = newExpense.Amount;
        TransactionDate = newExpense.TransactionDate;
        Status = newExpense.Status;
        Category = newExpense.Category;
        UpdatedAt = DateTime.Now;
    }

    public override string ToString(){
        return $"Id: {Id}, Description: {Description}, Amount: {Amount}, TransactionDate: {TransactionDate}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, Category: {Category}";
    }
}