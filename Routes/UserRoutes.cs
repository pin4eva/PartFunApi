using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Model;
using PartyFunApi.Repositories;

namespace PartyFunApi.Routes;

public static class UserRoutes
{
  public static RouteGroupBuilder MapUserRoute(this IEndpointRouteBuilder routeBuilder)
  {
    var group = routeBuilder.MapGroup("api/v1/users")
                            .WithParameterValidation().WithName("UserGroup")
                            .WithTags("Users")
                            .WithOpenApi();

    group.MapGet("/", async (IUserRepository userRepo, IMapper mapper) =>
    {
      var users = await userRepo.GetUsers();
      return Results.Ok(users);
    }).WithName("GetUsers");

    group.MapGet("id/{id}", async (int id, IUserRepository userRepo, IMapper mapper) =>
       {
         var users = await userRepo.GetUserById(id);
         return Results.Ok(users);
       }).WithName("GetUserById");

    group.MapGet("guid/{guid}", async (Guid guid, IUserRepository userRepo, IMapper mapper) =>
           {
             var users = await userRepo.GetUserByGuid(guid);
             return Results.Ok(users);
           }).WithName("GetUserByGuid");


    group.MapDelete("guid/{guid}", async (Guid guid, DataContext userRepo, IMapper mapper) =>
               {
                 var user = await userRepo.Users.Where(user => user.Guid == guid).FirstOrDefaultAsync();
                 if (user is null) return Results.NotFound("user not found");
                 return Results.Ok(user.Id);
               }).WithName("DeleteUserByGuid");


    group.MapPost("/", async (IAuthService authService, IUserRepository userRepo, DataContext db, CreateUserDTO input, IMapper mapper) =>
    {
      var email = input.Email.ToLower();
      if (email is null) return Results.BadRequest("Email is required");
      var existingUser = await db.Users.Where((user) => user.Email == email).FirstOrDefaultAsync();

      if (existingUser is not null) return Results.BadRequest("User with email already exist");

      var password = input?.Password ?? "defaultPassword";

      var pass = authService.CreatePassword(password);
      if (input is null) return Results.BadRequest("Input must not be null");
      User user = new()
      {
        Name = input.Name,
        Email = email,
        Gender = input.Gender,
        PasswordSalt = pass.PasswordSalt,
        PasswordHash = pass.PasswordHash,
        IsEmailVerified = false
      };

      db.Users.Add(user);


      await db.SaveChangesAsync();

      GetUsersDTO mappedUser = mapper.Map<GetUsersDTO>(user);

      return Results.CreatedAtRoute("GetUserById", new { Id = user.Id }, mappedUser);
    }
    ).WithName("CreateUsers");


    group.MapPut("/", async (UpdateUserDTO input, IMapper mapper, DataContext db) =>
      {
        var user = await db.Users.Where((user) => user.Guid == input.Guid).FirstOrDefaultAsync();
        mapper.Map(input, user);

        await db.SaveChangesAsync();
        return Results.NoContent();

      }

      ).WithName("UpdateUser");
    return group;
  }
}
