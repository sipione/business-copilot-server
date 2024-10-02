using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
using APP.UseCases;
using APP.Exceptions;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CashFlowController : ControllerBase{
    private readonly ILogger<CashFlowController> _logger;
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly GetAllIncomesUseCase _getAllIncomesUseCase;
    private readonly GetIncomeByIdUseCase _getIncomeByIdUseCase;
    private readonly CreateIncomeUseCase _createIncomeUseCase;
    private readonly UpdateIncomeUseCase _updateIncomeUseCase;
    private readonly DeleteIncomeUseCase _deleteIncomeUseCase;

    public CashFlowController(ILogger<CashFlowController> logger, ICashFlowRepository cashFlowRepository, GetAllIncomesUseCase getAllIncomesUseCase, GetIncomeByIdUseCase getIncomeByIdUseCase, CreateIncomeUseCase createIncomeUseCase, UpdateIncomeUseCase updateIncomeUseCase, DeleteIncomeUseCase deleteIncomeUseCase){
        _logger = logger;
        _cashFlowRepository = cashFlowRepository;
        _getAllIncomesUseCase = getAllIncomesUseCase;
        _getIncomeByIdUseCase = getIncomeByIdUseCase;
        _createIncomeUseCase = createIncomeUseCase;
        _updateIncomeUseCase = updateIncomeUseCase;
        _deleteIncomeUseCase = deleteIncomeUseCase;
    }

    [HttpGet("income", Name = "GetIncomeCashFlows")]
    public async Task<IActionResult> GetIncomeCashFlows([FromHeader] Guid userId, [FromHeader] string token)
    {
        try{
            IEnumerable<IncomeCashFlow> cashFlows = await _getAllIncomesUseCase.Execute(userId, token);
            return StatusCode(200, cashFlows);
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
    public async Task<IActionResult> GetExpenseCashFlows()
    {
        try{
            IEnumerable<ExpenseCashFlow> cashFlows = await _cashFlowRepository.GetExpenseCashFlows();
            return StatusCode(200, cashFlows);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("income/{id}", Name = "GetIncomeCashFlowById")]
    public async Task<IActionResult> GetIncomeCashFlowById([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] string token)
    {
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

    [HttpGet("expense/{id}", Name = "GetExpenseCashFlowById")]
    public async Task<IActionResult> GetExpenseCashFlowById(Guid id)
    {
        try{
            ExpenseCashFlow cashFlow = await _cashFlowRepository.GetExpenseCashFlowById(id);
            if(cashFlow == null){
                return StatusCode(404, "Expense Cash Flow not found");
            }
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("income", Name = "CreateIncomeCashFlow")]
    public async Task<IActionResult> CreateIncomeCashFlow([FromForm] CreateIncomeCashFlowDto dto, [FromHeader] Guid userId, [FromHeader] string token){
        IncomeCashFlow cashFlow;

        try{
            cashFlow = new IncomeCashFlow(dto.ContractId, userId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, $"Error creating IncomeCashFlow: {e.Message}");
        }

        try{
            await _cashFlowRepository.CreateIncomeCashFlow(cashFlow);
            return StatusCode(201, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("expense", Name = "CreateExpenseCashFlow")]
    public async Task<IActionResult> CreateExpenseCashFlow([FromBody] CreateExpenseCashFlowDto dto){
        ExpenseCashFlow cashFlow = new ExpenseCashFlow(dto.ContractId, dto.UserId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);

        try{
            await _cashFlowRepository.CreateExpenseCashFlow(cashFlow);
            return StatusCode(201, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
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
    public async Task<IActionResult> UpdateExpenseCashFlow([FromBody] UpdateExpenseCashFlowDto dto){

        try{
            ExpenseCashFlow? expenseCashFlowExists = await _cashFlowRepository.GetExpenseCashFlowById(dto.Id);
            if(expenseCashFlowExists == null){
                return StatusCode(404, "CashFlow not found");
            }
            expenseCashFlowExists.ContractId = dto.ContractId ?? expenseCashFlowExists.ContractId;
            expenseCashFlowExists.Description = dto.Description ?? expenseCashFlowExists.Description;
            expenseCashFlowExists.Amount = dto.Amount ?? expenseCashFlowExists.Amount;
            expenseCashFlowExists.TransactionDate = dto.TransactionDate ?? expenseCashFlowExists.TransactionDate;
            expenseCashFlowExists.Status = dto.Status ?? expenseCashFlowExists.Status;
            expenseCashFlowExists.Category = dto.Category ?? expenseCashFlowExists.Category;
            await _cashFlowRepository.UpdateExpenseCashFlow(expenseCashFlowExists);
            return StatusCode(200, expenseCashFlowExists);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("income/{id}", Name = "DeleteIncomeCashFlow")]
    public async Task<IActionResult> DeleteIncomeCashFlow([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] string token)
    {
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
    public async Task<IActionResult> DeleteExpenseCashFlow(Guid id)
    {
        try{
            bool cashFlow = await _cashFlowRepository.DeleteExpenseCashFlow(id);
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }
}