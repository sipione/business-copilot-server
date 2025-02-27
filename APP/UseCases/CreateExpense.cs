using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class CreateExpenseUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public CreateExpenseUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<ExpenseCashFlow> Execute(Guid userId, string token, ExpenseCashFlow newExpense){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
        }

        if(!_userAuthorizationService.AuthorizeCreateCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to create expenses");
        }

        ExpenseCashFlow createdExpense = await _cashFlowRepository.CreateExpenseCashFlow(newExpense);
        
        return createdExpense;
    }
}