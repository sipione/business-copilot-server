using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class ContractController : ControllerBase{
    private readonly ILogger<ContractController> _logger;
    private readonly IContractRepository _contractRepository;

    public ContractController(ILogger<ContractController> logger, IContractRepository contractRepository)
    {
        _logger = logger;
        _contractRepository = contractRepository;
    }

    [HttpGet(Name = "GetContracts")]
    public async Task<IEnumerable<Contract>> Get()
    {
        return await _contractRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetContract")]
    public async Task<Contract> Get(Guid id)
    {
        return await _contractRepository.GetById(id);
    }

    [HttpPost(Name = "CreateContract")]
    public async Task<Contract> Create(Contract contract)
    {
        return await _contractRepository.Create(contract);
    }

    [HttpPut(Name = "UpdateContract")]
    public async Task<Contract> Update(Contract contract)
    {
        return await _contractRepository.Update(contract);
    }

    [HttpDelete("{id}", Name = "DeleteContract")]
    public async Task<Contract> Delete(Guid id)
    {
        return await _contractRepository.Delete(id);
    }
}