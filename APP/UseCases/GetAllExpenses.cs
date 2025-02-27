using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class GetAllExpensesUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetAllExpensesUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<IEnumerable<ExpenseCashFlow>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
        }

        if(!_userAuthorizationService.AuthorizeViewCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to get expenses");
        }

        IEnumerable<ExpenseCashFlow> expenses = await _cashFlowRepository.GetExpenseCashFlows(user.Id);
        
        return expenses;
    }
}