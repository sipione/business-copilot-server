using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class UpdateStakeholderUseCase{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public UpdateStakeholderUseCase(IStakeholderRepository stakeholderRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _stakeholderRepository = stakeholderRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task<Stakeholder> Execute(Guid userId, string token, Stakeholder newStakeholderInstance, Guid stakeholderId){
        User userAuthenticated = await _authenticationService.Authenticate(userId, token);

        if(userAuthenticated == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials provided are not valid");
        }

        if(!_userAuthorizationService.AuthorizeUpdateStakeholders(userAuthenticated)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to update this stakeholder");
        }

        Stakeholder stakeholder = await _stakeholderRepository.GetById(stakeholderId, userAuthenticated.Id);

        if(stakeholder == null){
            throw CommonExceptions.NotFound("Stakeholder not found");
        }

        stakeholder = newStakeholderInstance;
        stakeholder.UpdatedAt = DateTime.Now;

        return await _stakeholderRepository.Update(stakeholder);
    }
}