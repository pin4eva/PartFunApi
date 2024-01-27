
using PartyFunApi.DTO;
using PartyFunApi.Model;

namespace PartyFunApi.Repositories;

public interface IAuthService
{
  CreatePasswordResponse CreatePassword(string password);
  bool ComparePasssword(string PlainPassword, byte[] PasswordHash, byte[] PasswordSalt);
  string CreateToken(User user);
}
