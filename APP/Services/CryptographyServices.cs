using System.Security.Cryptography;
using System.Text;
using APP.Entities;
using APP.Interfaces.Services;

namespace APP.Services;

public class CryptographyServices : ICryptographyServices
{
    public string Encrypt(string text)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToBase64String(bytes);
    }

    public bool ValidateCryptography(string text, string cryptography)
    {
        return Encrypt(text) == cryptography;
    }

    public string GenerateToken(User user)
    {
        return Encrypt(user.Email + user.Password);
    }

    public bool ValidateToken(string token, User user)
    {
        return GenerateToken(user) == token;
    }
}
