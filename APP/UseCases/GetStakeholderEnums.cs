using APP.Entities;
using APP.Exceptions;
using APP.Enums;
using APP.Interfaces.Services;

namespace APP.UseCases;

public class GetStakeholderEnumsUseCase{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetStakeholderEnumsUseCase(IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }
    

    public async Task<Dictionary<string, List<string>>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewStakeholders(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view stakeholder enums");
        }
        
        List<string> stakeholderTypes = Enum.GetValues(typeof(StakeholderType)).Cast<StakeholderType>().Select(s => s.ToString()).ToList();
        List<string> accountStatuses = Enum.GetValues(typeof(AccountStatus)).Cast<AccountStatus>().Select(s => s.ToString()).ToList();
        Dictionary<string, List<string>> stakeholderEnums = new(){
            {"stakeholderTypes", stakeholderTypes},
            {"accountStatuses", accountStatuses}
        };

        return stakeholderEnums;
    }
}