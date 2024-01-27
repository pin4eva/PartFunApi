using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Extensions;
using PartyFunApi.Model;
using PartyFunApi.Repositories;

namespace PartyFunApi.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController(DataContext db, ICategoryRepo categoryRepo) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult> GetCategories()
  {
    var categories = await categoryRepo.GetCategories();
    return Ok(categories);
  }

  [HttpGet("id/{id}"), EndpointName("GetCategoryById")]
  public async Task<ActionResult> GetCategoryById(int id)
  {
    var categories = await categoryRepo.GetCategoryById(id);
    return Ok(categories);
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDTO input)
  {

    var existingCategory = await db.Categories.Where((c) => c.Name.ToLower() == input.Name.ToLower()).SingleOrDefaultAsync();

    if (existingCategory is not null) return BadRequest("Duplicate category name exist");

    var slug = input.Name.Slugify();

    Category category = new()
    {
      Guid = Guid.NewGuid(),
      Slug = slug,
      Name = input.Name
    };
    db.Categories.Add(category);
    await db.SaveChangesAsync();


    return category;
  }

  [Authorize]
  [HttpPut]
  public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDTO input)
  {
    var category = await db.Categories.FindAsync(input.Id);

    if (category is null) return NotFound("Invalid category id");

    category.Name = input.Name;
    category.Slug = input.Name.Slugify();

    await db.SaveChangesAsync();

    return category;
  }

  [Authorize]
  [HttpDelete("{id}")]
  public async Task<ActionResult<int>> DeleteCategory(int id)
  {
    var category = await db.Categories.FindAsync(id);
    if (category is null) return NotFound("Invalid category id");

    db.Categories.Remove(category);
    await db.SaveChangesAsync();

    return category.Id;

  }
}
