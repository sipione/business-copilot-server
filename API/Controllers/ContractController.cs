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

    public ContractController(ILogger<ContractController> logger, IContractRepository contractRepository, GetAllContractsUseCase getAllContractsUseCase)
    {
        _logger = logger;
        _contractRepository = contractRepository;
        this._getAllContractsUsecase = getAllContractsUseCase;
    }

    [HttpGet(Name = "GetContracts")]
    public async Task<IActionResult> Get([FromHeader] Guid userId, [FromHeader] string token){
        try{
            IEnumerable<Contract>? result = await _getAllContractsUsecase.Execute(userId, token);
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
    public async Task<IActionResult> Create(CreateContractDto createContractDto)
    {
        try{
            var contract = new Contract(
                createContractDto.UserId,
                createContractDto.StakeholderId,
                createContractDto.Title,
                createContractDto.Description,
                createContractDto.InitialAmount,
                createContractDto.Discount,
                createContractDto.Installments,
                createContractDto.Interest,
                createContractDto.Penalty,
                createContractDto.TotalAmount,
                createContractDto.PaidAmount,
                createContractDto.RemainingAmount,
                createContractDto.DocumentPath,
                createContractDto.VoucherId,
                createContractDto.PaymentStatus,
                createContractDto.ContractStatus,
                createContractDto.StartDate,
                createContractDto.EndDate
            );
            var createdContract = await _contractRepository.Create(contract);
            return CreatedAtRoute("GetContract", new { id = createdContract.Id }, createdContract);
        }catch(Exception ex){
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut(Name = "UpdateContract")]
    public async Task<IActionResult> Update(UpdateContractDto updateContractDto)
    {
        try{
            var contract = await _contractRepository.GetById(updateContractDto.Id);
            if(contract == null){
                return NotFound();
            }
            contract.StakeholderId = updateContractDto.StakeholderId ?? contract.StakeholderId;
            contract.Title = updateContractDto.Title ?? contract.Title;
            contract.Description = updateContractDto.Description ?? contract.Description;
            contract.InitialAmount = updateContractDto.InitialAmount ?? contract.InitialAmount;
            contract.Discount = updateContractDto.Discount ?? contract.Discount;
            contract.Installments = updateContractDto.Installments ?? contract.Installments;
            contract.Interest = updateContractDto.Interest ?? contract.Interest;
            contract.Penalty = updateContractDto.Penalty ?? contract.Penalty;
            contract.TotalAmount = updateContractDto.TotalAmount ?? contract.TotalAmount;
            contract.PaidAmount = updateContractDto.PaidAmount ?? contract.PaidAmount;
            contract.RemainingAmount = updateContractDto.RemainingAmount ?? contract.RemainingAmount;
            contract.DocumentPath = updateContractDto.DocumentPath ?? contract.DocumentPath;
            contract.VoucherId = updateContractDto.VoucherId ?? contract.VoucherId;
            contract.PaymentStatus = updateContractDto.PaymentStatus ?? contract.PaymentStatus;
            contract.ContractStatus = updateContractDto.ContractStatus ?? contract.ContractStatus;
            contract.StartDate = updateContractDto.StartDate ?? contract.StartDate;
            contract.EndDate = updateContractDto.EndDate ?? contract.EndDate;
            var updatedContract = await _contractRepository.Update(contract);
            return Ok(updatedContract);
        }catch(Exception ex){
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