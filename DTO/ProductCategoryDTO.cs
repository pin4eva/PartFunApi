using System.ComponentModel.DataAnnotations;

namespace PartyFunApi.DTO;

public class GetProductCategory
{
  public int Id { get; set; }
  public Guid Guid { get; set; } = Guid.NewGuid();
  public required string Name { get; set; }
  public required string Slug { get; set; }
  public int CategoryId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public record CreateProductCategoryDTO
([Required] string Name,
[Required] int CategoryId
);


public record UpdateProductCategoryDTO
([Required] string Name,
[Required] int CategoryId,
[Required] int Id
);
