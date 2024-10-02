using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;
public class GetAllIncomesUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetAllIncomesUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<IEnumerable<IncomeCashFlow>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view incomes");
        }

        IEnumerable<IncomeCashFlow> incomes = await _cashFlowRepository.GetIncomeCashFlows();

        if(incomes == null || !incomes.Any()){
            throw CommonExceptions.NotFound("Not found: there are no incomes");
        }

        return incomes;
    }
}