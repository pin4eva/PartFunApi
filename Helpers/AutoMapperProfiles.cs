

using AutoMapper;
using PartyFunApi.DTO;
using PartyFunApi.Model;

namespace PartyFunApi.Helpers;

public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<User, GetUserDTO>();
    CreateMap<User, GetUsersDTO>();
    CreateMap<CreateUserDTO, User>();
    CreateMap<UpdateUserDTO, User>();

    // Category
    CreateMap<Category, GetCategoryDTO>();
    CreateMap<CreateCategoryDTO, Category>();
    CreateMap<UpdateCategoryDTO, Category>();

    // Product Category

    CreateMap<ProductCategory, GetProductCategory>();
  }
}
