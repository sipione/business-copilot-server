using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Services;

namespace APP.UseCases;

public class DeleteUserUseCase{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public DeleteUserUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task Execute(Guid userId, string token, Guid requestedId, Action<string> DeleteProfilePicture){
        User userAuthenticated = await _authenticationService.Authenticate(requestedId, token);

        if(userAuthenticated == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials provided are not valid");
        }

        if((userAuthenticated.Id == userId && !_userAuthorizationService.AuthorizeDeleteUsers(userAuthenticated, userId)) || (userAuthenticated.Id != userId && !_userAuthorizationService.AuthorizeDeleteUsers(userAuthenticated, null))){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to delete users");
        }

        User user = await _userRepository.GetById(userId);
        if(user == null){
            throw CommonExceptions.NotFound("User not found");
        }

        if(user.ProfilePicture != null){
            DeleteProfilePicture(user.ProfilePicture);
        }

        await _userRepository.Delete(userId);

        return;
    }
}