using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class CreateIncomeUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public CreateIncomeUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<IncomeCashFlow> Execute(Guid userId, string token, IncomeCashFlow newIncome){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeCreateCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to create incomes");
        }

        IncomeCashFlow createdIncome = await _cashFlowRepository.CreateIncomeCashFlow(newIncome);
        
        return createdIncome;
    }
}