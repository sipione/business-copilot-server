using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;

namespace APP.UseCases;
public class GetUserByIdUseCase{
    
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetUserByIdUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }
    public async Task<User> Execute(Guid userId, Guid requestUserId, string token){
        User userAuthenticated = await _authenticationService.Authenticate(requestUserId, token);

        if(userAuthenticated == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if((userAuthenticated.Id == userId && !_userAuthorizationService.AuthorizeViewUsers(userAuthenticated, userId)) || (userAuthenticated.Id != userId && !_userAuthorizationService.AuthorizeViewUsers(userAuthenticated, null))){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view users");
        }

        User user = await _userRepository.GetById(userId);
        if(user == null){
            throw CommonExceptions.NotFound("User not found");
        }
        return user;
    }
}
