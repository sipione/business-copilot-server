using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Services;

namespace APP.UseCases;

public class LoginUserUseCase{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ICryptographyServices _cryptographyServices;

    public LoginUserUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, ICryptographyServices cryptographyServices){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _cryptographyServices = cryptographyServices;
    }

    public async Task<User> OpenSession(string email, string password){

        User? user = await _authenticationService.Login(email, password);

        if(user == null){
            throw CommonExceptions.Unauthorized("Unauthorized: credentials provided are not valid");
        }

        return user;
    }

    public string GenerateToken(Guid userId, string email){
        return _cryptographyServices.GenerateToken(userId, email);
    }
}