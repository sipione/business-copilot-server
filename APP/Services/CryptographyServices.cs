using System.Security.Cryptography;
using System.Text;
using APP.Entities;
using APP.Interfaces.Services;

namespace APP.Services;

public class CryptographyServices : ICryptographyServices
{
    public string Encrypt(string text)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text)); // Corrigido o nome da classe para SHA256
        return Convert.ToBase64String(bytes); // Use Base64 para converter o byte[] de volta para string
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
