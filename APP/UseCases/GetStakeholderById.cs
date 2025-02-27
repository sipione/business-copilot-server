using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class GetStakeholderByIdUseCase{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetStakeholderByIdUseCase(IStakeholderRepository stakeholderRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _stakeholderRepository = stakeholderRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<Stakeholder> Execute(Guid userId, string token, Guid id){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
        }

        if(!_userAuthorizationService.AuthorizeViewStakeholders(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view stakeholders");
        }

        Stakeholder? stakeholder = await _stakeholderRepository.GetById(id, user.Id);

        if(stakeholder == null){
            throw CommonExceptions.NotFound("Stakeholder not found");
        }

        return stakeholder;
    }
}