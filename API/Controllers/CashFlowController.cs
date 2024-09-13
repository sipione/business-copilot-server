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

    [HttpGet(Name = "GetCashFlows")]
    public async Task<IEnumerable<CashFlow>> Get()
    {
        return await _cashFlowRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetCashFlow")]
    public async Task<CashFlow> Get(Guid id)
    {
        return await _cashFlowRepository.GetById(id);
    }

    [HttpPost(Name = "CreateCashFlow")]
    public async Task<CashFlow> Create(CashFlow cashFlow)
    {
        return await _cashFlowRepository.Create(cashFlow);
    }

    [HttpPut(Name = "UpdateCashFlow")]
    public async Task<CashFlow> Update(CashFlow cashFlow)
    {
        return await _cashFlowRepository.Update(cashFlow);
    }

    [HttpDelete("{id}", Name = "DeleteCashFlow")]
    public async Task<CashFlow> Delete(Guid id)
    {
        return await _cashFlowRepository.Delete(id);
    }
}