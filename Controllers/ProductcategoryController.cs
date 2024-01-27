using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Extensions;
using PartyFunApi.Model;

namespace PartyFunApi.Controllers;

[ApiController]
[Route("api/v1/product-categories")]
public class ProductcategoryController(DataContext db) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<List<ProductCategory>>> GetProductcategories()
  {
    var productCategories = await db.ProductCategories.ToListAsync();
    return productCategories;
  }

  [HttpGet("id/{id}")]
  public async Task<ActionResult<ProductCategory>> GetProductcategory(int id)
  {
    var productCategory = await db.ProductCategories.FindAsync(id);
    if (productCategory is null) return NotFound("Product category not found");

    return productCategory;
  }
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<ProductCategory>> CreateProductCategory(CreateProductCategoryDTO input)
  {
    var existingProductCategory = await db.ProductCategories.Where((pc) => pc.Name.ToLower() == input.Name.ToLower()).FirstOrDefaultAsync();
    if (existingProductCategory is not null) return BadRequest("Duplication product category exist already");

    ProductCategory category = new()
    {
      Guid = Guid.NewGuid(),
      Slug = input.Name.Slugify(),
      Name = input.Name,
      CategoryId = input.CategoryId
    };

    db.ProductCategories.Add(category);
    await db.SaveChangesAsync();

    return category;
  }


  [Authorize]
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
