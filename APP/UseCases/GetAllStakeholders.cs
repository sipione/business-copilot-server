using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class GetAllStakeholdersUsecase{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetAllStakeholdersUsecase(IStakeholderRepository stakeholderRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _stakeholderRepository = stakeholderRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<IEnumerable<Stakeholder>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewStakeholders(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view stakeholders");
        }

        IEnumerable<Stakeholder> stakeholders = await _stakeholderRepository.GetAll(user.Id);

        if(stakeholders == null || !stakeholders.Any()){
            throw CommonExceptions.NotFound("Not found: no stakeholders found");
        }

        return stakeholders;
    }
}