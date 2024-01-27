using System.ComponentModel.DataAnnotations;

namespace PartyFunApi.DTO;

public record CreateProductCategoryDTO
([Required] string Name,
[Required] int CategoryId
);


public record UpdateProductCategoryDTO
([Required] string Name,
[Required] int CategoryId,
[Required] int Id
);
