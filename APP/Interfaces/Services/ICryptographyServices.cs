using APP.Entities; 
namespace APP.Interfaces.Services;
public interface ICryptographyServices
{
    string Encrypt(string text);
    bool ValidateCryptography(string text, string cryptography);
    string GenerateToken(User user);
    bool ValidateToken(string token, User user);
}