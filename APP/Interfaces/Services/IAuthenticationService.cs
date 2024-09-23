using System;
using APP.Entities;

namespace APP.Interfaces.Services;

public interface IAuthenticationService{
    Task<User> Authenticate(Guid userId, string token);
    Task<User> Login(string email, string password);
}