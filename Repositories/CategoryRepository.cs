using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;

namespace PartyFunApi.Repositories;


public class CategoryRepository(DataContext db, IMapper mapper) : ICategoryRepo
{
  public async Task<List<GetCategoryDTO>> GetCategories()
  {
    var categories = await db.Categories.ProjectTo<GetCategoryDTO>(mapper.ConfigurationProvider).ToListAsync();

    return categories;
  }

  public async Task<GetCategoryDTO?> GetCategoryById(int id)
  {
    var category = await db.Categories.ProjectTo<GetCategoryDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync((c) => c.Id == id);
    return category;
  }

  public async Task<GetCategoryDTO?> GetCategoryBySlug(string slug)
  {
    var category = await db.Categories.ProjectTo<GetCategoryDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync((c) => c.Slug == slug);

    return category;
  }

  public async Task<bool> SaveAllAsync()
  {
    return await db.SaveChangesAsync() > 0;
  }
}
