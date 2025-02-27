using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class DeleteIncomeUseCase {
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public DeleteIncomeUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<bool> Execute(Guid userId, string token, Guid incomeId){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
        }

        if(!_userAuthorizationService.AuthorizeDeleteCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to delete incomes");
        }

        bool deleted = await _cashFlowRepository.DeleteIncomeCashFlow(incomeId);
        
        if(!deleted){
            throw CommonExceptions.NotFound("Not found: income not found");
        }
        
        return deleted;
    }
}