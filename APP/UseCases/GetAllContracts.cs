using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class GetAllContractsUseCase{
    private IUserAuthorizationService _userAuthorizationService;
    private IAuthenticationService _authenticationService;
    private IContractRepository _contractRepository;

    public GetAllContractsUseCase(IUserAuthorizationService userAuthorizationService, IAuthenticationService authenticationService, IContractRepository contractRepository){
        this._userAuthorizationService = userAuthorizationService;
        this._authenticationService = authenticationService;
        this._contractRepository = contractRepository;
    }

    public async Task<IEnumerable<Contract>> Execute(Guid userId, string token){
        User? user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid to see the contracts");
        }

        bool isAuthorized = _userAuthorizationService.AuthorizeViewContracts(user);
        if(!isAuthorized){
            throw CommonExceptions.Forbidden("Forbidden: the user cannot see the contracts");
        }

        IEnumerable<Contract>? contracts = await _contractRepository.GetAll(user.Id);
        if(contracts == null || contracts.Count() < 1){
            throw CommonExceptions.NotFound("NOT FOUND: Was not possible to find any contract");
        }

        return contracts;
    }
    public async Task<IEnumerable<Contract>> ExecuteTest(Guid userId){

        IEnumerable<Contract>? contracts = await _contractRepository.GetAll(userId);
        if(contracts == null || contracts.Count() < 1){
            throw CommonExceptions.NotFound("NOT FOUND: Was not possible to find any contract");
        }

        return contracts;
    }
}