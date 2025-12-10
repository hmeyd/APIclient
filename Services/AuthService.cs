using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ClientApi.Models;

public class AuthService
{
    private readonly string _jwtKey;

    public AuthService(IConfiguration config)
    {
        _jwtKey = config["JwtKey"] ?? throw new Exception("JwtKey not found in configuration.");
    }

    // HASH DU MOT DE PASSE
    public string HashPassword(string password)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_jwtKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hash);
    }

    // VÉRIFICATION DU MOT DE PASSE
    public bool VerifyPassword(string password, string storedHash)
    {
        var hash = HashPassword(password);
        return hash == storedHash;
    }

    // CRÉATION DU JWT
    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(5),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
