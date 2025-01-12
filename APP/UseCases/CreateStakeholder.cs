using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Exceptions;

namespace APP.UseCases
{
    public class CreateStakeholderUseCase{
        private readonly IStakeholderRepository _stakeholderRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserAuthorizationService _userAuthorizationService;

        public CreateStakeholderUseCase(IStakeholderRepository stakeholderRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService)
        {
            _stakeholderRepository = stakeholderRepository;
            _authenticationService = authenticationService;
            _userAuthorizationService = userAuthorizationService;
        }

        public async Task<Stakeholder> Execute(Guid userId, string token, Stakeholder newStakeholder){
            User user = await _authenticationService.Authenticate(userId, token);

            if(user == null)
            {
                throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
            }

            if(!_userAuthorizationService.AuthorizeCreateStakeholders(user))
            {
                throw CommonExceptions.Forbidden("Forbidden: you don't have permission to create stakeholders");
            }

            if(newStakeholder.Email == null || newStakeholder.Name == null || newStakeholder.Type == null || newStakeholder.Status == null)
            {
                throw CommonExceptions.BadRequest("Bad Request: email, name, type and status are required");
            }

            Stakeholder createdStakeholder = await _stakeholderRepository.Create(newStakeholder);
            
            return createdStakeholder;
        }
    }
}