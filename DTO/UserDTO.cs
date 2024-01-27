

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PartyFunApi.DTO;

public record CreateUserDTO(
[Required] string Name,
[Required] string Email,
[AllowNull] string? Password,
[Required] string Gender
);


public record UpdateUserDTO(
int Id,
 string Name,
 Guid Guid,
 string Email,
 string Gender
);

public record GetUsersDTO(
int Id,
 string Name,
 Guid Guid,
 string Email,
 string Gender
);


public record GetUserDTO(
int Id,
 string Name,
 Guid Guid,
 string Email,
 string Gender
);

