using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class ProgressController : ControllerBase{
    private readonly ILogger<ProgressController> _logger;
    private readonly IProgressRepository _progressRepository;

    public ProgressController(ILogger<ProgressController> logger, IProgressRepository progressRepository)
    {
        _logger = logger;
        _progressRepository = progressRepository;
    }

    [HttpGet(Name = "GetProgresses")]
    public async Task<IEnumerable<Progress>> Get()
    {
        return await _progressRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetProgress")]
    public async Task<Progress> Get(Guid id)
    {
        return await _progressRepository.GetById(id);
    }

    [HttpPost(Name = "CreateProgress")]
    public async Task<Progress> Create(Progress progress)
    {
        return await _progressRepository.Create(progress);
    }

    [HttpPut(Name = "UpdateProgress")]
    public async Task<Progress> Update(Progress progress)
    {
        return await _progressRepository.Update(progress);
    }

    [HttpDelete("{id}", Name = "DeleteProgress")]
    public async Task<Progress> Delete(Guid id)
    {
        return await _progressRepository.Delete(id);
    }
}