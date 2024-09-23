using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Services;

namespace APP.UseCases;

public class UpdateUserUseCase{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public UpdateUserUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }

    public async Task Execute(Guid userId, string token, Guid userRequestId, User userUpdated){
        User userAuthenticated = await _authenticationService.Authenticate(userRequestId, token);

        if(userAuthenticated == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials provided are not valid");
        }

        if(userAuthenticated.Id == userId){
            await UpdateYourself(userId, userAuthenticated, userUpdated);
        }else{
            await UpdateAnotherUser(userId, userAuthenticated, userUpdated);
        }

        return;
    }

    private async Task UpdateYourself(Guid userId, User userAuthenticated, User userUpdated){
        if(!_userAuthorizationService.AuthorizeUpdateUsers(userAuthenticated, userId)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to update users");
        }

        userAuthenticated.Name = userUpdated.Name;
        userAuthenticated.Email = userUpdated.Email;
        userAuthenticated.Status = userUpdated.Status;
        userAuthenticated.UpdatedAt = DateTime.Now;

        await _userRepository.Update(userAuthenticated);

        return;
    }

    private async Task UpdateAnotherUser(Guid userId, User userAuthenticated, User userUpdated){
        if(!_userAuthorizationService.AuthorizeUpdateUsers(userAuthenticated, null)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to update users");
        }

        User user = await _userRepository.GetById(userId);

        if(user == null){
            throw CommonExceptions.NotFound("User not found");
        }

        user.Name = userUpdated.Name;
        user.Email = userUpdated.Email;
        user.Role = userUpdated.Role;
        user.AllowPermitionsByRole();
        user.Status = userUpdated.Status;
        user.UpdatedAt = DateTime.Now;

        await _userRepository.Update(user);

        return;
    }
}