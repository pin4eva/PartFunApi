using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Model;

namespace PartyFunApi.Repositories;

public class UserRepository(DataContext db, IMapper mapper) : IUserRepository
{

  public async Task<List<GetUsersDTO>> GetUsers()
  {
    var users = await db.Users
.ProjectTo<GetUsersDTO>(mapper.ConfigurationProvider)
.ToListAsync();

    return users;
  }

  public async Task<GetUserDTO?> GetUserByGuid(Guid guid)
  {
    var user = await db.Users
.Where((user) => user.Guid == guid).ProjectTo<GetUserDTO>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
    return user;
  }

  public async Task<GetUserDTO?> GetUserById(int id)
  {
    var user = await db.Users.Where(user => user.Id == id).ProjectTo<GetUserDTO>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
    return user;
  }

  public async Task<User?> GetUser(int id)
  {
    var user = await db.Users.FindAsync(id);
    return user;
  }

  public async Task<bool> SaveAllAsync()
  {
    return await db.SaveChangesAsync() > 0;
  }

  public async Task<User?> GetUserByEmail(string email)
  {
    return await db.Users.Where((user) => user.Email == email).FirstOrDefaultAsync();
  }
}
