using Microsoft.AspNetCore.Mvc;
using APP.Entities;
using APP.UseCases;
using APP.Exceptions;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class StakeholderController : ControllerBase{
    private readonly ILogger<StakeholderController> _logger;
    private readonly GetAllStakeholdersUsecase _getAllStakeholdersUsecase;
    private readonly GetStakeholderByIdUseCase _getStakeholderByIdUseCase;
    private readonly DeleteStakeholderUseCase _deleteStakeholderUseCase;
    private readonly CreateStakeholderUseCase _createStakeholderUseCase;
    private readonly UpdateStakeholderUseCase _updateStakeholderUseCase;
    private readonly GetStakeholderEnumsUseCase _getStakeholderEnumsUseCase;

    public StakeholderController(ILogger<StakeholderController> logger, GetAllStakeholdersUsecase getAllStakeholdersUsecase, GetStakeholderByIdUseCase getStakeholderByIdUseCase, DeleteStakeholderUseCase deleteStakeholderUseCase, CreateStakeholderUseCase createStakeholderUseCase, UpdateStakeholderUseCase updateStakeholderUseCase, GetStakeholderEnumsUseCase getStakeholderEnumsUseCase){
        _logger = logger;
        _getAllStakeholdersUsecase = getAllStakeholdersUsecase;
        _getStakeholderByIdUseCase = getStakeholderByIdUseCase;
        _deleteStakeholderUseCase = deleteStakeholderUseCase;
        _createStakeholderUseCase = createStakeholderUseCase;
        _updateStakeholderUseCase = updateStakeholderUseCase;
        _getStakeholderEnumsUseCase = getStakeholderEnumsUseCase;
    }

    [HttpGet(Name = "GetStakeholders")]
    public async Task<IActionResult> Get([FromHeader] Guid userId, [FromHeader] string token){
        try{
            IEnumerable<Stakeholder> stakeholders = await _getAllStakeholdersUsecase.Execute(userId, token);
            return StatusCode(200, stakeholders);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if(e is CommonExceptions commonExceptions){
                return StatusCode(commonExceptions.StatusCode, commonExceptions.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}", Name = "GetStakeholder")]
    public async Task<IActionResult> Get([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] string token){
        try{
            Stakeholder stakeholder = await _getStakeholderByIdUseCase.Execute(userId, token, id);
            return StatusCode(200, stakeholder);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if(e is CommonExceptions commonExceptions){
                return StatusCode(commonExceptions.StatusCode, commonExceptions.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("enums", Name = "GetStakeholderEnums")]
    public async Task<IActionResult> GetEnums([FromHeader] Guid userId, [FromHeader] string token){
        try{
            Dictionary<string, List<string>> stakeholderEnums = await _getStakeholderEnumsUseCase.Execute(userId, token);
            return StatusCode(200, stakeholderEnums);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if(e is CommonExceptions commonExceptions){
                return StatusCode(commonExceptions.StatusCode, commonExceptions.Message);
            }
            return StatusCode(500, e.Message);
        }
    }
    

    [HttpPost(Name = "CreateStakeholder")]
    public async Task<IActionResult> Create([FromForm] CreateStakeholderDto stakeholderDto, [FromHeader] Guid userId, [FromHeader] string token){
        try{
            Stakeholder stakeholder = new Stakeholder(
                userId, 
                stakeholderDto.Name, 
                stakeholderDto.Email, 
                stakeholderDto.Phone, 
                stakeholderDto.Address, 
                stakeholderDto.City, 
                stakeholderDto.State, 
                stakeholderDto.Country, 
                stakeholderDto.ZipCode, 
                stakeholderDto.Type, 
                stakeholderDto.Status
            );

            Stakeholder createdStakeholder = await _createStakeholderUseCase.Execute(userId, token, stakeholder);

            return StatusCode(201, createdStakeholder);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if(e is CommonExceptions commonExceptions){
                return StatusCode(commonExceptions.StatusCode, commonExceptions.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut(Name = "UpdateStakeholder")]
    public async Task<IActionResult> Update([FromForm] UpdateStakeholderDto stakeholderDto, [FromForm] Guid userId, [FromForm] string token){
        try{
            Stakeholder newStakeholder = new Stakeholder(userId, stakeholderDto.Name, stakeholderDto.Email, stakeholderDto.Phone, stakeholderDto.Address, stakeholderDto.City, stakeholderDto.State, stakeholderDto.Country, stakeholderDto.ZipCode, stakeholderDto.Type, stakeholderDto.Status);

            Stakeholder updatedStakeholder = await _updateStakeholderUseCase.Execute(userId, token, newStakeholder, stakeholderDto.Id);

            return StatusCode(200, updatedStakeholder);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if(e is CommonExceptions commonExceptions){
                return StatusCode(commonExceptions.StatusCode, commonExceptions.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeleteStakeholder")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromHeader] Guid userId, [FromHeader] String token){
        try{
            await _deleteStakeholderUseCase.Execute(userId, token, id);
            return StatusCode(204);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if(e is CommonExceptions commonExceptions){
                return StatusCode(commonExceptions.StatusCode, commonExceptions.Message);
            }
            return StatusCode(500, e.Message);
        }
    }
}