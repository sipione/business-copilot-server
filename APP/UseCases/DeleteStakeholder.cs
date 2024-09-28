using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases;

public class DeleteStakeholderUseCase{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public DeleteStakeholderUseCase(IStakeholderRepository stakeholderRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _stakeholderRepository = stakeholderRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task Execute(Guid userId, string token, Guid id){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeDeleteStakeholders(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to delete stakeholders");
        }

        bool? result = await _stakeholderRepository.Delete(id);

        if(result == null){
            throw CommonExceptions.NotFound("Stakeholder not found");
        }
    }
}