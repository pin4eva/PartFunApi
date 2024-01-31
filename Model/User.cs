
using System.Text.Json.Serialization;

namespace PartyFunApi.Model;

public class User
{
  public int Id { get; set; }
  public Guid Guid { get; set; } = Guid.NewGuid();
  public required string Name { get; set; } = string.Empty;
  public required string Gender { get; set; } = string.Empty;
  public string Avatar { get; set; } = string.Empty;
  public string AvatarPublicId { get; set; } = string.Empty;
  public required string Email { get; set; }
  public bool IsEmailVerified { get; set; }
  [JsonIgnore]
  public byte[] PasswordHash { get; set; } = [];
  [JsonIgnore]
  public byte[] PasswordSalt { get; set; } = [];
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
