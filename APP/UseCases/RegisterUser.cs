using APP.Entities;
using APP.Exceptions;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;

namespace APP.UseCases;

public class RegisterUserUseCase{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ICryptographyServices _cryptographyServices;

    public RegisterUserUseCase(IUserRepository userRepository, IAuthenticationService authenticationService, ICryptographyServices cryptographyServices){
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _cryptographyServices = cryptographyServices;
    }

    public async Task<User> Execute(User newUser){
        User userExists = await _userRepository.GetByEmail(newUser.Email);
        
        if (userExists != null){
            throw CommonExceptions.Conflict("User already exists");
        }

        newUser.AllowPermitionsByRole();

        await _userRepository.Create(newUser);

        return newUser;
    }
}