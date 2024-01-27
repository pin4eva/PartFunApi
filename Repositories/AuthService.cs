using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PartyFunApi.DTO;
using PartyFunApi.Model;

namespace PartyFunApi.Repositories;


public class AuthService(IConfiguration config) : IAuthService
{
  public CreatePasswordResponse CreatePassword(string password)
  {
    using HMACSHA512 hmac = new();

    return new CreatePasswordResponse
    {
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
      PasswordSalt = hmac.Key
    };
  }
  public bool ComparePasssword(string PlainPassword, byte[] PasswordHash, byte[] PasswordSalt)
  {
    using HMACSHA512 hmac = new(PasswordSalt);

    byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(PlainPassword));

    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != PasswordHash[i]) return false;
    }

    return true;
  }

  public string CreateToken(User user)
  {
    string secretKey = config["JWT_SECRET"] ?? throw new NullReferenceException("JWT_SECRET not found");
    SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secretKey));
    List<Claim> claims =
    [
        new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
          new(JwtRegisteredClaimNames.Email, user.Email)
      ];

    SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);
    SecurityTokenDescriptor tokenDescriptor = new()
    {
      Subject = new ClaimsIdentity(claims),
      SigningCredentials = creds,
      Expires = DateTime.Now.AddDays(7),
    };

    JwtSecurityTokenHandler tokenHandler = new();
    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);

  }
}
