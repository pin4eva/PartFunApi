

namespace PartyFunApi.Model;

public class Product
{
  public int Id { get; set; }
  public Guid Guid { get; set; } = Guid.NewGuid();
  public required string Sku { get; set; } // 00001
  public required string Name { get; set; }
  public string Slug { get; set; } = string.Empty;
  public string? Size { get; set; } = string.Empty;
  public string? Color { get; set; } = string.Empty;
  public List<string> Tags { get; set; } = [];
  public required string Brand { get; set; }
  public required string Description { get; set; }
  public int Price { get; set; }
  public int ProductCategoryId { get; set; }
  public ProductCategory ProductCategory { get; set; } = null!;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public int AddedById { get; set; }
  public int? UpdatedById { get; set; }
  public User AddedBy { get; set; } = null!;
  public User? UpdatedBy { get; set; }

  public int GroupId { get; set; }
  public required ProductGroup Group { get; set; }
}
