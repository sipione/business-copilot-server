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
    public async Task<IActionResult> Get()
    {
        try{
            var stakeholders = await _stakeholderRepository.GetAll();
            return StatusCode(200, stakeholders);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}", Name = "GetStakeholder")]
    public async Task<IActionResult> Get(Guid id)
    {
        try{
            var stakeholder = await _stakeholderRepository.GetById(id);
            return StatusCode(200, stakeholder);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost(Name = "CreateStakeholder")]
    public async Task<IActionResult> Create(CreateStakeholderDto stakeholderDto)
    {
        try{
            Stakeholder stakeholderExist = await _stakeholderRepository.GetByEmail(stakeholderDto.Email);
            if(stakeholderExist != null){
                return StatusCode(400, "Stakeholder already exists");
            }
            var stakeholder = new Stakeholder(stakeholderDto.UserId, stakeholderDto.Name, stakeholderDto.Email, stakeholderDto.Phone, stakeholderDto.Address, stakeholderDto.City, stakeholderDto.State, stakeholderDto.Country, stakeholderDto.ZipCode, stakeholderDto.Type, stakeholderDto.Status);
            var createdStakeholder = await _stakeholderRepository.Create(stakeholder);
            return StatusCode(201, createdStakeholder);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut(Name = "UpdateStakeholder")]
    public async Task<IActionResult> Update(UpdateStakeholderDto stakeholderDto)
    {
        try{
            var stakeholder = await _stakeholderRepository.GetById(stakeholderDto.Id);
            if(stakeholder == null){
                return StatusCode(404, "Stakeholder not found");
            }
            
            stakeholder.Name = stakeholderDto.Name ?? stakeholder.Name;
            stakeholder.Email = stakeholderDto.Email ?? stakeholder.Email;
            stakeholder.Phone = stakeholderDto.Phone ?? stakeholder.Phone;
            stakeholder.Address = stakeholderDto.Address ?? stakeholder.Address;
            stakeholder.City = stakeholderDto.City ?? stakeholder.City;
            stakeholder.State = stakeholderDto.State ?? stakeholder.State;
            stakeholder.Country = stakeholderDto.Country ?? stakeholder.Country;
            stakeholder.ZipCode = stakeholderDto.ZipCode ?? stakeholder.ZipCode;
            stakeholder.Type = stakeholderDto.Type ?? stakeholder.Type;
            stakeholder.Status = stakeholderDto.Status ?? stakeholder.Status;

            var updatedStakeholder = await _stakeholderRepository.Update(stakeholder);
            return StatusCode(200, updatedStakeholder);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeleteStakeholder")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try{
            var stakeholder = await _stakeholderRepository.Delete(id);
            if(stakeholder == null){
                return StatusCode(404, "Stakeholder not found");
            }
            return StatusCode(200, stakeholder);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }
}