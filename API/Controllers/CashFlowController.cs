using Microsoft.AspNetCore.Mvc;
using APP.Entities;
using APP.UseCases;
using APP.Exceptions;
using APP.Enums;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CashFlowController : ControllerBase{
    private readonly ILogger<CashFlowController> _logger;
    private readonly GetAllIncomesUseCase _getAllIncomesUseCase;
    private readonly GetIncomeByIdUseCase _getIncomeByIdUseCase;
    private readonly CreateIncomeUseCase _createIncomeUseCase;
    private readonly UpdateIncomeUseCase _updateIncomeUseCase;
    private readonly DeleteIncomeUseCase _deleteIncomeUseCase;
    private readonly GetAllExpensesUseCase _getAllExpensesUseCase;
    private readonly GetExpenseByIdUseCase _getExpenseByIdUseCase;
    private readonly CreateExpenseUseCase _createExpenseUseCase;
    private readonly UpdateExpenseUseCase _updateExpenseUseCase;
    private readonly DeleteExpenseUseCase _deleteExpenseUseCase;
    private readonly GetIncomeCategoriesUseCase _getIncomeCategoriesUseCase;
    private readonly GetExpensesCategoriesUseCase _getExpensesCategoriesUseCase;
    private readonly GetCashflowStatusUseCase _getCashflowStatusUseCase;

    public CashFlowController(ILogger<CashFlowController> logger, GetAllIncomesUseCase getAllIncomesUseCase, GetIncomeByIdUseCase getIncomeByIdUseCase, CreateIncomeUseCase createIncomeUseCase, UpdateIncomeUseCase updateIncomeUseCase, DeleteIncomeUseCase deleteIncomeUseCase, GetAllExpensesUseCase getAllExpensesUseCase, GetExpenseByIdUseCase getExpenseByIdUseCase, CreateExpenseUseCase createExpenseUseCase, UpdateExpenseUseCase updateExpenseUseCase, DeleteExpenseUseCase deleteExpenseUseCase, GetIncomeCategoriesUseCase getIncomeCategoriesUseCase, GetExpensesCategoriesUseCase getExpensesCategoriesUseCase, GetCashflowStatusUseCase getCashflowStatusUseCase){
        _logger = logger;
        _getAllIncomesUseCase = getAllIncomesUseCase;
        _getIncomeByIdUseCase = getIncomeByIdUseCase;
        _createIncomeUseCase = createIncomeUseCase;
        _updateIncomeUseCase = updateIncomeUseCase;
        _deleteIncomeUseCase = deleteIncomeUseCase;
        _getAllExpensesUseCase = getAllExpensesUseCase;
        _getExpenseByIdUseCase = getExpenseByIdUseCase;
        _createExpenseUseCase = createExpenseUseCase;
        _updateExpenseUseCase = updateExpenseUseCase;
        _deleteExpenseUseCase = deleteExpenseUseCase;
        _getIncomeCategoriesUseCase = getIncomeCategoriesUseCase;
        _getExpensesCategoriesUseCase = getExpensesCategoriesUseCase;
        _getCashflowStatusUseCase = getCashflowStatusUseCase;
    }

    [HttpGet("income", Name = "GetIncomeCashFlows")]
    public async Task<IActionResult> GetIncomeCashFlows([FromHeader] Guid userId, [FromHeader] string token){
        try{
            IEnumerable<IncomeCashFlow> cashFlows = await _getAllIncomesUseCase.Execute(userId, token);
            var resultFormatted = cashFlows.Select(cf => new {
                cf.Id,
                cf.ContractId,
                cf.Description,
                cf.Amount,
                cf.TransactionDate,
                cf.Status,
                cf.Category
            });
            return StatusCode(200, resultFormatted);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("expense", Name = "GetExpenseCashFlows")]
    public async Task<IActionResult> GetExpenseCashFlows([FromHeader] Guid userId, [FromHeader] string token){
        try{
            IEnumerable<ExpenseCashFlow> cashFlows = await _getAllExpensesUseCase.Execute(userId, token);
            var resultFormatted = cashFlows.Select(cf => new {
                cf.Id,
                cf.ContractId,
                cf.Description,
                cf.Amount,
                cf.TransactionDate,
                cf.Status,
                cf.Category
            });
            return StatusCode(200, resultFormatted);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("income/{id}", Name = "GetIncomeCashFlowById")]
    public async Task<IActionResult> GetIncomeCashFlowById([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] string token){
        try{
            IncomeCashFlow cashFlow = await _getIncomeByIdUseCase.Execute(userId, token, id);
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("expense/{expenseId}", Name = "GetExpenseCashFlowById")]
    public async Task<IActionResult> GetExpenseCashFlowById([FromRoute] Guid expenseId, [FromHeader] Guid userId, [FromHeader] string token){
        try{
            ExpenseCashFlow cashFlow = await _getExpenseByIdUseCase.Execute(userId, token, expenseId);
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("income", Name = "CreateIncomeCashFlow")]
    public async Task<IActionResult> CreateIncomeCashFlow([FromForm] CreateIncomeCashFlowDto dto, [FromHeader] Guid userId, [FromHeader] string token){
        IncomeCashFlow cashFlow;

        try{
            cashFlow = new IncomeCashFlow(dto.ContractId, userId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);
            IncomeCashFlow result = await _createIncomeUseCase.Execute(userId, token, cashFlow);
            return StatusCode(201, result);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("expense", Name = "CreateExpenseCashFlow")]
    public async Task<IActionResult> CreateExpenseCashFlow([FromForm] CreateExpenseCashFlowDto dto, [FromHeader] Guid userId, [FromHeader] string token){
        ExpenseCashFlow cashFlow;

        try{
            cashFlow = new ExpenseCashFlow(dto.ContractId, userId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);
            ExpenseCashFlow result = await _createExpenseUseCase.Execute(userId, token, cashFlow);
            return StatusCode(201, result);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("income", Name = "UpdateIncomeCashFlow")]
    public async Task<IActionResult> UpdateIncomeCashFlow([FromForm] UpdateIncomeCashFlowDto dto, [FromHeader] Guid userId, [FromHeader] string token){
        IncomeCashFlow newIncomeCashFlow;

        try{
            newIncomeCashFlow = new IncomeCashFlow(dto.ContractId, userId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);
            newIncomeCashFlow.Id = dto.Id;
            
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, $"Error creating IncomeCashFlow: {e.Message}");
        }

        try{
            IncomeCashFlow updatedIncomeCashFlow = await _updateIncomeUseCase.Execute(userId, token, newIncomeCashFlow);
            return StatusCode(200, updatedIncomeCashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("expense", Name = "UpdateExpenseCashFlow")]
    public async Task<IActionResult> UpdateExpenseCashFlow([FromForm] UpdateExpenseCashFlowDto dto, [FromHeader] Guid userId, [FromHeader] string token){
        ExpenseCashFlow newExpenseCashFlow;

        try{
            newExpenseCashFlow = new ExpenseCashFlow(dto.ContractId, userId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);
            newExpenseCashFlow.Id = dto.Id;

        }catch(Exception e){
            _logger.LogError(e.Message);    
            return StatusCode(500, $"Error creating ExpenseCashFlow: {e.Message}");
        }

        try{
            ExpenseCashFlow updatedExpenseCashFlow = await _updateExpenseUseCase.Execute(userId, token, newExpenseCashFlow);
            return StatusCode(200, updatedExpenseCashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("income/{id}", Name = "DeleteIncomeCashFlow")]
    public async Task<IActionResult> DeleteIncomeCashFlow([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] string token){
        try{
            bool cashFlow = await _deleteIncomeUseCase.Execute(userId, token, id);
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("expense/{id}", Name = "DeleteExpenseCashFlow")]
    public async Task<IActionResult> DeleteExpenseCashFlow([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] string token){
        try{
            bool cashFlow = await _deleteExpenseUseCase.Execute(userId, token, id);
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("enums", Name="GetCashFlowEnums")]
    public async Task<IActionResult> GetCashFlowEnums([FromHeader] Guid userId, [FromHeader] string token){
        IEnumerable<IncomeCategory> incomeCategories;
        IEnumerable<ExpensesCategory> expenseCategories;
        IEnumerable<PaymentStatus> cashflowStatus;

        try{
            incomeCategories = await _getIncomeCategoriesUseCase.Execute(userId, token);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
        try{
            expenseCategories = await _getExpensesCategoriesUseCase.Execute(userId, token);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
        try{
            cashflowStatus = await _getCashflowStatusUseCase.Execute(userId, token);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }

        return StatusCode(200, new {
            incomeCategories,
            expenseCategories,
            cashflowStatus
        });
    }
}