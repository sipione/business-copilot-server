using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class StakeholderController : ControllerBase{
    private readonly ILogger<StakeholderController> _logger;
    private readonly IStakeholderRepository _stakeholderRepository;

    public StakeholderController(ILogger<StakeholderController> logger, IStakeholderRepository stakeholderRepository)
    {
        _logger = logger;
        _stakeholderRepository = stakeholderRepository;
    }

    [HttpGet(Name = "GetStakeholders")]
    public async Task<IEnumerable<Stakeholder>> Get()
    {
        return await _stakeholderRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetStakeholder")]
    public async Task<Stakeholder> Get(Guid id)
    {
        return await _stakeholderRepository.GetById(id);
    }

    [HttpPost(Name = "CreateStakeholder")]
    public async Task<Stakeholder> Create(Stakeholder stakeholder)
    {
        return await _stakeholderRepository.Create(stakeholder);
    }

    [HttpPut(Name = "UpdateStakeholder")]
    public async Task<Stakeholder> Update(Stakeholder stakeholder)
    {
        return await _stakeholderRepository.Update(stakeholder);
    }

    [HttpDelete("{id}", Name = "DeleteStakeholder")]
    public async Task<Stakeholder> Delete(Guid id)
    {
        return await _stakeholderRepository.Delete(id);
    }
}