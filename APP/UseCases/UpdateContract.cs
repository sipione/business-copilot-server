using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;
public class UpdateContractUseCase{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;
    private readonly IContractRepository _contractRepository; 
    public UpdateContractUseCase(IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService, IContractRepository contractRepository){
        this._authenticationService = authenticationService;
        this._userAuthorizationService = userAuthorizationService;
        this._contractRepository = contractRepository;
    }

    public async Task<Contract?> Execute(Guid userId, Contract updatedContract, string token){
        Contract? result = null;
        User? userGoten = await this._authenticationService.Authenticate(userId, token);

        if(userGoten == null){
            throw CommonExceptions.Unauthorized("Credentials provided not approved.");
        }

        Contract? originalContract = null;

        try{
            originalContract = await this._contractRepository.GetById(updatedContract.Id);

            if(originalContract == null){
                throw CommonExceptions.NotFound($"Contract with id {updatedContract.Id} was no found.");
            }
        }catch(Exception e){
            throw;
        }

        if(!this._userAuthorizationService.AuthorizeUpdateContracts(userGoten, originalContract.UserId)){
            throw CommonExceptions.Forbidden("You don't have permissions to update contracts");
        }
        
        try{
            updatedContract.Id = originalContract.Id;
            updatedContract.UserId = originalContract.UserId;
            updatedContract.CreatedAt = originalContract.CreatedAt;
            updatedContract.UpdatedAt = DateTime.Now;

            result = await this._contractRepository.Update(updatedContract);
        }catch(Exception e){
            throw;
        }

        return result;
    }
}