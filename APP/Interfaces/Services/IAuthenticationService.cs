using System;
using APP.Entities;

public interface IAuthenticationService{
    Task<User> Authenticate(string email, Guid userId, string token);
    Task<User> Login(string email, string password);
}