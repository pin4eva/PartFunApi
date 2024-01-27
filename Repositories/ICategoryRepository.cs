
using PartyFunApi.DTO;

namespace PartyFunApi.Repositories;

public interface ICategoryRepo
{
  Task<List<GetCategoryDTO>> GetCategories();

  Task<GetCategoryDTO?> GetCategoryById(int id);
  Task<GetCategoryDTO?> GetCategoryBySlug(string slug);
  Task<bool> SaveAllAsync();
}
