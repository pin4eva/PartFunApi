

namespace PartyFunApi.Model;

public class ProductGroup
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public string Slug { get; set; } = string.Empty;
  public List<string> CategoriesList { get; set; } = [];
}
