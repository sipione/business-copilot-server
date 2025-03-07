using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Entities;
using APP.UseCases;
using APP.Exceptions;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class ContractController : ControllerBase{
    private readonly ILogger<ContractController> _logger;
    private readonly IContractRepository _contractRepository;
    private readonly GetAllContractsUseCase _getAllContractsUsecase;
    private readonly CreateContractUseCase _createContractUseCase;
    private readonly UpdateContractUseCase _updateContractUseCase;

    public ContractController(ILogger<ContractController> logger, IContractRepository contractRepository, GetAllContractsUseCase getAllContractsUseCase, CreateContractUseCase createContractUseCase, UpdateContractUseCase updateContractUseCase)
    {
        _logger = logger;
        _contractRepository = contractRepository;
        this._getAllContractsUsecase = getAllContractsUseCase;
        this._createContractUseCase = createContractUseCase;
        this._updateContractUseCase = updateContractUseCase;
    }

    [HttpGet(Name = "GetContracts")]
    public async Task<IActionResult> Get([FromHeader] Guid userId, [FromHeader] string token){
        try{
            IEnumerable<Contract>? result = await _getAllContractsUsecase.Execute(userId, token);
            // IEnumerable<Contract>? result = await _getAllContractsUsecase.ExecuteTest(userId);
            return Ok(result);
        }catch(Exception ex){
            if(ex is CommonExceptions commonException){
                return StatusCode(commonException.StatusCode, commonException.Message);
            }

            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id}", Name = "GetContract")]
    public async Task<IActionResult> Get(Guid id)
    {
        try{
            var contract = await _contractRepository.GetById(id);
            if(contract == null)
                return NotFound();
            return Ok(contract);
        }catch(Exception ex){
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost(Name = "CreateContract")]
    public async Task<IActionResult> Create([FromForm] CreateContractDto createContractDto){
        try{
            var newContract = new Contract(
                createContractDto.UserId,
                createContractDto.StakeholderId,
                createContractDto.Title,
                createContractDto.Description ?? string.Empty,
                createContractDto.InitialAmount,
                createContractDto.Discount,
                createContractDto.Installments,
                createContractDto.Interest,
                createContractDto.Penalty,
                createContractDto.TotalAmount,
                createContractDto.PaidAmount,
                createContractDto.RemainingAmount,
                createContractDto.DocumentPath ?? string.Empty,
                createContractDto.VoucherId,
                createContractDto.PaymentStatus,
                createContractDto.ContractStatus,
                createContractDto.StartDate,
                createContractDto.EndDate
            );

            Contract createdContract = await _createContractUseCase.Execute(newContract);

            return Ok(createdContract);
        }catch(Exception ex){
            if(ex is CommonExceptions ce){
                return StatusCode(ce.StatusCode, ce.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut(Name = "UpdateContract")]
    public async Task<IActionResult> Update([FromHeader]Guid userId, [FromHeader]string token, [FromForm] UpdateContractDto updateContractDto){
        try{     
            Contract newContract = new()
            {
                Id = updateContractDto.Id,
                UserId = updateContractDto.UserId,
                StakeholderId = updateContractDto.StakeholderId,
                Title = updateContractDto.Title ?? string.Empty,
                Description = updateContractDto.Description,
                InitialAmount = updateContractDto.InitialAmount,
                Discount = updateContractDto.Discount,
                Installments = updateContractDto.Installments,
                Interest = updateContractDto.Interest,
                Penalty = updateContractDto.Penalty,
                TotalAmount = updateContractDto.TotalAmount,
                PaidAmount = updateContractDto.PaidAmount,
                RemainingAmount = updateContractDto.RemainingAmount,
                DocumentPath = updateContractDto.DocumentPath,
                VoucherId = updateContractDto.VoucherId,
                PaymentStatus = updateContractDto.PaymentStatus,
                ContractStatus = updateContractDto.ContractStatus,
                StartDate = updateContractDto.StartDate,
                EndDate = updateContractDto.EndDate
            };
            
            Contract? result = await this._updateContractUseCase.Execute(userId, newContract, token);

            return Ok(result);
            
        }catch(Exception ex){
            if(ex is CommonExceptions ce){
                return StatusCode(ce.StatusCode, ce.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("{id}", Name = "DeleteContract")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try{
            var contract = await _contractRepository.Delete(id);
            if(contract == null)
                return NotFound();
            return Ok(contract);
        }catch(Exception ex){
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
}