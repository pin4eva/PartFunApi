namespace PartyFunApi.Model;

public class ProductCategory
{
  public int Id { get; set; }
  public Guid Guid { get; set; } = Guid.NewGuid();
  public required string Name { get; set; }
  public required string Slug { get; set; }
  public int CategoryId { get; set; }
  public Category Category { get; set; } = null!;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public virtual ICollection<Product> Products { get; set; } = [];
}
