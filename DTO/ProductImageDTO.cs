using System.ComponentModel.DataAnnotations;

namespace PartyFunApi.DTO;

public class GetProductImageDTO
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public string ImageUrl { get; set; } = string.Empty;
  public bool IsMain { get; set; } = false;
}


public class GetProductImagesDTO
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public string ImageUrl { get; set; } = string.Empty;
  public bool IsMain { get; set; } = false;
}

public class UploadImageTransformationInputDTO
{
  public int? Width { get; set; } = null;
  public int? Height { get; set; } = null;
}

public class UploadImageResponseDTO
{
  public required string PublicId { get; set; }
  [Url]
  public required string Url { get; set; }
  public string? ErrorMessage { get; set; }
}

public class UploadProductImageDTO
{
  public int ProductId { get; set; }

  public required IFormFileCollection Files { get; set; }
}


public record SetMainProductImageDTO(int Id);
