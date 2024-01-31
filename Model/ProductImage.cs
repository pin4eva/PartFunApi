namespace PartyFunApi.Model;

public class ProductImage
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public string PublidId { get; set; } = string.Empty;
  public Product Product { get; set; } = null!;

  public string ImageUrl { get; set; } = string.Empty;
  public bool IsMain { get; set; } = false;
}
