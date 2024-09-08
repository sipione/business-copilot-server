public class CashFlow
{
    public Guid Id { get; set; }
    public Contract Contract { get; set; }
    public User User { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public CashFlow(Contract contract, User user, string description, float amount, DateTime transactionDate, PaymentStatus status)
    {
        Id = Guid.NewGuid();
        Contract = contract;
        User = user;
        Description = description;
        Amount = amount;
        TransactionDate = transactionDate;
        Status = status;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public string toString()
    {
        return $"Id: {Id}, Description: {Description}, Amount: {Amount}, TransactionDate: {TransactionDate}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}

public class IncomeCashFlow : CashFlow
{
    public IncomeCategory Category { get; set; }

    public IncomeCashFlow(Contract contract, User user, string description, float amount, DateTime transactionDate, PaymentStatus status, IncomeCategory category) : base(contract, user, description, amount, transactionDate, status)
    {
        Category = category;
    }
}

public class ExpenseCashFlow : CashFlow
{
    public ExpensesCategory Category { get; set; }

    public ExpenseCashFlow(Contract contract, User user, string description, float amount, DateTime transactionDate, PaymentStatus status, ExpensesCategory category) : base(contract, user, description, amount, transactionDate, status)
    {
        Category = category;
    }
}