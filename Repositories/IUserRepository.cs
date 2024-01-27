
using PartyFunApi.DTO;
using PartyFunApi.Model;

namespace PartyFunApi.Repositories;

public interface IUserRepository
{
  Task<GetUserDTO?> GetUserById(int id);
  Task<User?> GetUser(int id);
  Task<User?> GetUserByEmail(string email);
  Task<GetUserDTO?> GetUserByGuid(Guid guid);
  Task<List<GetUsersDTO>> GetUsers();

  Task<bool> SaveAllAsync();
}
