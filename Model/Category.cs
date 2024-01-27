

namespace PartyFunApi.Model;

public class Category
{
  public int Id { get; set; }
  public Guid Guid { get; set; } = Guid.NewGuid();
  public required string Name { get; set; }
  public string Slug { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public virtual ICollection<ProductCategory> ProductCategories { get; set; } = [];
}
