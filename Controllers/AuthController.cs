using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyFunApi.DTO;
using PartyFunApi.Repositories;

namespace PartyFunApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService, IUserRepository userRepository) : ControllerBase
{
  [HttpPost("login")]
  public async Task<ActionResult> Login(LoginRequestDTO input)
  {
    var email = input.Email.ToLower();
    var user = await userRepository.GetUserByEmail(email);
    if (user is null) return NotFound("User not in record");

    if (!authService.ComparePasssword(input.Password, user.PasswordHash, user.PasswordSalt))
    { return Unauthorized("Incorrect email or password"); };

    var token = authService.CreateToken(user);
    LoginResponseDTO responseDTO = new() { Token = token };

    return Ok(responseDTO);
  }

  [HttpGet("me"), Authorize]
  public async Task<ActionResult> Me()
  {
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (id is null) return Unauthorized("id cannot be null");
    var user = await userRepository.GetUserById(int.Parse(id));
    if (user is null) return Unauthorized("Invalid user id supplied");

    return Ok(user);
  }
}
