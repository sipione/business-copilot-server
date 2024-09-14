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
    public async Task<IActionResult> Get()
    {
        try{
            IEnumerable<SubAccounts> subAccounts = await _subAccountRepository.GetAll();
            return Ok(subAccounts);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}", Name = "GetSubAccount")]
    public async Task<IActionResult> Get(Guid id)
    {
        try{
            SubAccounts subAccount = await _subAccountRepository.GetById(id);
            return Ok(subAccount);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost(Name = "CreateSubAccount")]
    public async Task<IActionResult> Create(CreateSubAccountDto subAccountDto)
    {
        try{
            SubAccounts subAccountExist = await _subAccountRepository.GetByEmail(subAccountDto.Email);
            if(subAccountExist != null){
                return StatusCode(400, "SubAccount already exists");
            }
            var subAccount = new SubAccounts(subAccountDto.UserId, subAccountDto.StakeholderId, subAccountDto.Name, subAccountDto.Description, subAccountDto.Email, subAccountDto.Password, subAccountDto.Role);
            var createdSubAccount = await _subAccountRepository.Create(subAccount);
            return StatusCode(201, createdSubAccount);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut(Name = "UpdateSubAccount")]
    public async Task<IActionResult> Update(UpdateSubAccountDto subAccountDto)
    {
        try{
            SubAccounts subAccount = await _subAccountRepository.GetById(subAccountDto.Id);
            if(subAccount == null){
                return StatusCode(404, "SubAccount not found");
            }
            if(subAccountDto.StakeholderId != null){
                subAccount.StakeholderId = (Guid)subAccountDto.StakeholderId;
            }
            if(subAccountDto.Name != null){
                subAccount.Name = subAccountDto.Name;
            }
            if(subAccountDto.Description != null){
                subAccount.Description = subAccountDto.Description;
            }
            if(subAccountDto.Email != null){
                subAccount.Email = subAccountDto.Email;
            }
            if(subAccountDto.Password != null){
                subAccount.Password = subAccountDto.Password;
            }
            if(subAccountDto.Status != null){
                subAccount.Status = (AccountStatus)subAccountDto.Status;
            }
            if(subAccountDto.Role != null){
                subAccount.Role = (SubAccountRole)subAccountDto.Role;
                subAccount.AllowPermitionsByRole();
            }
            var updatedSubAccount = await _subAccountRepository.Update(subAccount);
            return Ok(updatedSubAccount);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeleteSubAccount")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try{
            var subAccount = await _subAccountRepository.Delete(id);
            if(subAccount == null){
                return StatusCode(404, "SubAccount not found");
            }
            return Ok(subAccount);
        }catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }
}