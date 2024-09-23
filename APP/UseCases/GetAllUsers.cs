using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;

public class GetAllUsersUseCase{

    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public GetAllUsersUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, IUserAuthorizationService userAuthorizationService){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _userAuthorizationService = userAuthorizationService;
    }
    

    public async Task<IEnumerable<User>> Execute(Guid userId, string token){
        User user = await _authenticationService.Authenticate(userId, token);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials are not invalid");
        }

        if(!_userAuthorizationService.AuthorizeViewUsers(user, null)){
            throw CommonExceptions.Forbidden("Forbidden: you don't have permission to view users");
        }

        IEnumerable<User> users = await _userRepository.GetAll();
        return users;
    }

}