

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

    // Products

    CreateMap<Product, GetProductDTO>();

    CreateMap<Product, GetProductsDTO>().ForMember(dest => dest.Image, opt =>
    opt.MapFrom(src => src.Images.FirstOrDefault(image => image.IsMain).ImageUrl));
    CreateMap<CreateProductDTO, Product>();
    CreateMap<UpdateProductDTO, Product>();

    // Sales

    CreateMap<Sales, GetAllSalesDTO>();
    CreateMap<Sales, GetSingleSalesDTO>();
    CreateMap<CreateSalesDTO, Sales>();
    CreateMap<UpdateSalesDTO, Sales>();

    // Product Images
    CreateMap<ProductImage, GetProductImagesDTO>();
    CreateMap<ProductImage, GetProductImageDTO>();

  }
}
