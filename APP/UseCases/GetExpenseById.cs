using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class GetExpenseByIdUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetExpenseByIdUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<ExpenseCashFlow> Execute(Guid userId, string token, Guid expenseId){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to get expenses");
        }

        ExpenseCashFlow? expense = await _cashFlowRepository.GetExpenseCashFlowById(expenseId, user.Id);
        
        if(expense == null){
            throw CommonExceptions.NotFound("Not found: expense not found");
        }

        return expense;
    }
}