using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class GetIncomeByIdUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetIncomeByIdUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<IncomeCashFlow> Execute(Guid userId, string token, Guid incomeId){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view incomes");
        }

        IncomeCashFlow? income = await _cashFlowRepository.GetIncomeCashFlowById(incomeId, user.Id);

        if(income == null){
            throw CommonExceptions.NotFound("Not found: income not found");
        }
        
        return income;
    }
}