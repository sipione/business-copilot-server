using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CashFlowController : ControllerBase{
    private readonly ILogger<CashFlowController> _logger;
    private readonly ICashFlowRepository _cashFlowRepository;

    public CashFlowController(ILogger<CashFlowController> logger, ICashFlowRepository cashFlowRepository)
    {
        _logger = logger;
        _cashFlowRepository = cashFlowRepository;
    }

    [HttpGet("income", Name = "GetIncomeCashFlows")]
    public async Task<IActionResult> GetIncomeCashFlows()
    {
        try{
            IEnumerable<IncomeCashFlow> cashFlows = await _cashFlowRepository.GetIncomeCashFlows();
            return StatusCode(200, cashFlows);
        }catch(Exception e){
            _logger.LogError(e.Message);
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
    public async Task<IActionResult> GetIncomeCashFlowById(Guid id)
    {
        try{
            IncomeCashFlow cashFlow = await _cashFlowRepository.GetIncomeCashFlowById(id);
            if(cashFlow == null){
                return StatusCode(404, "Income Cash Flow not found");
            }
            return StatusCode(200, cashFlow);
        }catch(Exception e){
            _logger.LogError(e.Message);
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
    public async Task<IActionResult> CreateIncomeCashFlow([FromBody] CreateIncomeCashFlowDto dto){
        IncomeCashFlow cashFlow = new IncomeCashFlow(dto.ContractId, dto.UserId, dto.Description, dto.Amount, dto.TransactionDate, dto.Status, dto.Category);

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
    public async Task<IActionResult> UpdateIncomeCashFlow([FromBody] UpdateIncomeCashFlowDto dto){

        try{
            IncomeCashFlow incomeCashFlowExists = await _cashFlowRepository.GetIncomeCashFlowById(dto.Id);
            if(incomeCashFlowExists == null){
                return StatusCode(404, "CashFlow not found");
            }
            incomeCashFlowExists.ContractId = dto.ContractId ?? incomeCashFlowExists.ContractId;
            incomeCashFlowExists.Description = dto.Description ?? incomeCashFlowExists.Description;
            incomeCashFlowExists.Amount = dto.Amount ?? incomeCashFlowExists.Amount;
            incomeCashFlowExists.TransactionDate = dto.TransactionDate ?? incomeCashFlowExists.TransactionDate;
            incomeCashFlowExists.Status = dto.Status ?? incomeCashFlowExists.Status;
            incomeCashFlowExists.Category = dto.Category ?? incomeCashFlowExists.Category;
            await _cashFlowRepository.UpdateIncomeCashFlow(incomeCashFlowExists);
            return StatusCode(200, incomeCashFlowExists);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    // [HttpDelete("{id}", Name = "DeleteCashFlow")]
    // public async Task<CashFlow> Delete(Guid id)
    // {
    //     return await _cashFlowRepository.Delete(id);
    // }
}