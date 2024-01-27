
using System.ComponentModel.DataAnnotations;

namespace PartyFunApi.DTO;

public class CreatePasswordResponse
{
  public required byte[] PasswordHash;
  public required byte[] PasswordSalt;
};


public record LoginRequestDTO([Required] string Email, [Required] string Password);

public class LoginResponseDTO
{
  public required string Token { get; set; }
};
