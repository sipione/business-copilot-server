using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;

namespace APP.UseCases;

public class CreateUserUseCase{
    
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public CreateUserUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }
    
    public async Task<Guid> Execute(Guid userId, string token, User newUser){
        User user = await _authenticationService.Authenticate(userId, token);

        if (user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not valid");
        }

        if (!_userAuthorizationService.AuthorizeCreateUsers(user)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to create users");
        }

        // Check if user exists
        User userExists = await _userRepository.GetByEmail(newUser.Email);
        if (userExists != null){
            throw CommonExceptions.Conflict("User already exists");
        }

        await _userRepository.Create(newUser);

        return newUser.Id;
    }
}