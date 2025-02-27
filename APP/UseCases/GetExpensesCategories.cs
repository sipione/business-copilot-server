using APP.Entities;
using APP.Exceptions;
using APP.Enums;
using APP.Interfaces.Services;

namespace APP.UseCases;

public class GetExpensesCategoriesUseCase{

    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetExpensesCategoriesUseCase(IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }
    

    public async Task<IEnumerable<ExpensesCategory>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null)
        {
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
        }

        if(!_userAuthorizationService.AuthorizeViewCashFlows(user))
        {
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view expenses categories");
        }

        IEnumerable<ExpensesCategory> expensesCategories = Enum.GetValues(typeof(ExpensesCategory)).Cast<ExpensesCategory>();
        
        return expensesCategories;
    }

}