using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class SubAccountController : ControllerBase{
    private readonly ILogger<SubAccountController> _logger;
    private readonly ISubAccountRepository _subAccountRepository;

    public SubAccountController(ILogger<SubAccountController> logger, ISubAccountRepository subAccountRepository)
    {
        _logger = logger;
        _subAccountRepository = subAccountRepository;
    }

    [HttpGet(Name = "GetSubAccounts")]
    public async Task<IEnumerable<SubAccounts>> Get()
    {
        return await _subAccountRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetSubAccount")]
    public async Task<SubAccounts> Get(Guid id)
    {
        return await _subAccountRepository.GetById(id);
    }

    [HttpPost(Name = "CreateSubAccount")]
    public async Task<SubAccounts> Create(SubAccounts subAccount)
    {
        return await _subAccountRepository.Create(subAccount);
    }

    [HttpPut(Name = "UpdateSubAccount")]
    public async Task<SubAccounts> Update(SubAccounts subAccount)
    {
        return await _subAccountRepository.Update(subAccount);
    }

    [HttpDelete("{id}", Name = "DeleteSubAccount")]
    public async Task<SubAccounts> Delete(Guid id)
    {
        return await _subAccountRepository.Delete(id);
    }
}