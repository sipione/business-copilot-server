using APP.Entities; 
namespace APP.Interfaces.Services;
public interface ICryptographyServices
{
    string Encrypt(string text);
    bool ValidateCryptography(string text, string cryptography);
    string GenerateToken(Guid userId, string email);
    bool ValidateToken(string token, Guid userId, string email);
}