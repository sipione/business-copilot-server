using System;
using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;

namespace APP.Services;

public class AuthenticationService : IAuthenticationService{
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyServices _cryptographyServices;

    public AuthenticationService(IUserRepository userRepository, ICryptographyServices cryptographyServices)
    {
        _userRepository = userRepository;
        _cryptographyServices = cryptographyServices;
    }

    public async Task<User?> Authenticate(string email, Guid userId, string token)
    {
        User user = await _userRepository.GetByEmail(email);
        if (user == null)
        {
            return null;
        }

        if (!_cryptographyServices.ValidateToken(token, userId, email))
        {
            return null;
        }

        return user;
    }

    public async Task<User?> Login(string email, string password)
    {
        User user = await _userRepository.GetByEmail(email);

        if (user == null)
        {
           return null;
        }

        if (!_cryptographyServices.ValidateCryptography(password, user.Password))
        {
            return null;
        }

        return user;
    }
}