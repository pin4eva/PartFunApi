using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Extensions;
using PartyFunApi.Model;

namespace PartyFunApi.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/product-categories")]
public class ProductCategoryController(DataContext db, IMapper mapper) : ControllerBase
{
  [HttpGet, AllowAnonymous]
  public async Task<ActionResult<List<ProductCategory>>> GetProductcategories()
  {
    var productCategories = await db.ProductCategories.ToListAsync();
    return productCategories;
  }

  [HttpGet("id/{id}"), AllowAnonymous]
  public async Task<ActionResult<ProductCategory>> GetProductcategory(int id)
  {
    var productCategory = await db.ProductCategories.FindAsync(id);
    if (productCategory is null) return NotFound("Product category not found");

    return productCategory;
  }

  [HttpPost]
  public async Task<ActionResult<GetProductCategory>> CreateProductCategory(CreateProductCategoryDTO input)
  {
    var existingProductCategory = await db.ProductCategories.Where((pc) => pc.Name.ToLower() == input.Name.ToLower()).FirstOrDefaultAsync();
    if (existingProductCategory is not null) return BadRequest("Duplication product category exist already");

    var category = await db.Categories.FindAsync(input.CategoryId);
    if (category is null) return NotFound("Please input a valid category id");
    ProductCategory productCategory = new()
    {
      Guid = Guid.NewGuid(),
      Slug = input.Name.Slugify(),
      Name = input.Name,
      CategoryId = category.Id
    };

    db.ProductCategories.Add(productCategory);
    await db.SaveChangesAsync();
    var mappedCat = mapper.Map<GetProductCategory>(productCategory);
    return mappedCat;
  }



  [HttpPut]
  public async Task<ActionResult<ProductCategory>> UpdateProductCategory(UpdateProductCategoryDTO input)
  {
    var productCat = await db.ProductCategories.FindAsync(input.Id);
    if (productCat is null) return NotFound("Invalid product category id");

    productCat.Name = input.Name;
    productCat.CategoryId = input.CategoryId;
    productCat.Slug = input.Name.Slugify();

    await db.SaveChangesAsync();

    return productCat;
  }

  [HttpDelete("id/{id}")]
  public async Task<ActionResult<int>> DeleteProductcategory(int id)
  {
    var productCategory = await db.ProductCategories.FindAsync(id);
    if (productCategory is null) return NotFound("Product category not found");

    db.ProductCategories.Remove(productCategory);

    await db.SaveChangesAsync();
    return id;
  }
}
