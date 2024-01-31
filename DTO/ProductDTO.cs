using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PartyFunApi.DTO;

public class GetProductsDTO
{
  public int Id { get; set; }
  public Guid Guid { get; set; } = Guid.NewGuid();
  public required string Sku { get; set; } // 00001
  public required string Name { get; set; }
  public string Slug { get; set; } = string.Empty;
  public string Image { get; set; } = string.Empty;
  public string? Size { get; set; } = string.Empty;
  public string? Color { get; set; } = string.Empty;
  public required string Brand { get; set; }
  public Decimal Price { get; set; }
  public int ProductCategoryId { get; set; }
  public DateTime? ExpiryDate { get; set; }
}


public class GetProductDTO : GetProductsDTO
{

  public List<string> Tags { get; set; } = [];
  public required string Description { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public int MinimumQuantity { get; set; }
  public int AddedById { get; set; }
  public int? UpdatedById { get; set; }
  public List<GetProductImagesDTO> Images { get; set; } = [];
}


public class CreateProductDTO

{

  [Required] public required string Sku { get; set; } // 00001
  [Required] public required string Name { get; set; }
  [Required] public required string Description { get; set; }
  [Required] public required string Brand { get; set; }
  public string? Size { get; set; } = string.Empty;
  public string? Color { get; set; } = string.Empty;
  [Range(100, 999999.99)] public Decimal Price { get; set; }
  public int Quantity { get; set; }
  public DateTime? ExpiryDate { get; set; }
  public int MinimumQuantity { get; set; }
  public int ProductCategoryId { get; set; }
  public List<string> Tags { get; set; } = [];
}


public class UpdateProductDTO
{
  public int Id { get; set; }
  public string? Sku { get; set; } // 00001
  public string? Name { get; set; }
  public string? Description { get; set; }
  public string? Brand { get; set; }
  public string? Size { get; set; } = string.Empty;
  public string? Color { get; set; } = string.Empty;
  public Decimal? Price { get; set; }
  public int? Quantity { get; set; }
  public DateTime? ExpiryDate { get; set; }
  public int? MinimumQuantity { get; set; }
  public int ProductCategoryId { get; set; }
  public List<string>? Tags { get; set; } = [];
}

