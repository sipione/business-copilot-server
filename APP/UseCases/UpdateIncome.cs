using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class UpdateIncomeUseCase{
    private readonly ICashFlowRepository _cashFlowRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public UpdateIncomeUseCase(ICashFlowRepository cashFlowRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _cashFlowRepository = cashFlowRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<IncomeCashFlow> Execute(Guid userId, string token, IncomeCashFlow newIncome){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeUpdateCashFlows(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to update incomes");
        }

        IncomeCashFlow? existingIncome = await _cashFlowRepository.GetIncomeCashFlowById(newIncome.Id, user.Id);

        if(existingIncome == null){
            throw CommonExceptions.NotFound("Not found: income not found");
        }

        existingIncome.UpdateFields(newIncome);

        IncomeCashFlow updatedIncome = await _cashFlowRepository.UpdateIncomeCashFlow(existingIncome);
        
        return updatedIncome;
    }
}