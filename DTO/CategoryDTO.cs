
using System.ComponentModel.DataAnnotations;

namespace PartyFunApi.DTO;


public class GetCategoryDTO
{
  public int Id { get; set; }
  public Guid Guid { get; set; }
  public string Slug { get; set; } = string.Empty;
  public required string Name { get; set; }
  public DateTime CreatedAt { get; set; }
};


public record CreateCategoryDTO([Required, MinLength(4)] string Name);
public record UpdateCategoryDTO([Required, MinLength(4)] string Name, int Id);
