using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class UpdateExpenseUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public UpdateExpenseUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<ExpenseCashFlow> Execute(Guid userId, string token, ExpenseCashFlow newExpense){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeUpdateCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to update expenses");
        }

        ExpenseCashFlow? existingExpense = await _cashFlowRepository.GetExpenseCashFlowById(newExpense.Id);

        if(existingExpense == null){
            throw CommonExceptions.NotFound("Not found: expense not found");
        }

        existingExpense.UpdateFields(newExpense);

        ExpenseCashFlow updatedExpense = await _cashFlowRepository.UpdateExpenseCashFlow(existingExpense);

        return updatedExpense;
    }
}