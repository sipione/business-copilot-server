using APP.Entities;
using APP.Exceptions;
using APP.Enums;
using APP.Interfaces.Services;

namespace APP.UseCases;

public class GetCashflowStatusUseCase{

    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetCashflowStatusUseCase(IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }
    

    public async Task<IEnumerable<PaymentStatus>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view cashflow status");
        }

        IEnumerable<PaymentStatus> cashflowStatus = Enum.GetValues(typeof(PaymentStatus)).Cast<PaymentStatus>();
        
        return cashflowStatus;
    }

}