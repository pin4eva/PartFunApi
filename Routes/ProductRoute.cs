using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Extensions;
using PartyFunApi.Model;

namespace PartyFunApi.Routes;

public static class ProductRoute
{
  public static RouteGroupBuilder MapProductRoute(this IEndpointRouteBuilder routeBuilder)
  {
    var group = routeBuilder.MapGroup("api/v1/products")
                                .WithParameterValidation().WithName("Products")
                                .WithTags("Products")
                                .WithOpenApi();


    // create product
    group.MapPost("/", async (CreateProductDTO input, DataContext db, ClaimsPrincipal claimsPrincipal, IMapper mapper) =>
    {
      var existingProduct = await db.Products.Where(p => p.Sku == input.Sku).SingleOrDefaultAsync();
      if (existingProduct is not null) return Results.BadRequest("Product with the same Sku");
      var slug = (input.Brand + " " + input.Name + " " + input.Sku).Slugify();
      Guid guid = Guid.NewGuid();

      var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (id is null) return Results.BadRequest("id cannot be null");



      Product product = new()
      {
        Slug = slug,
        Guid = guid,
        Name = input.Name,
        Description = input.Description,
        Sku = input.Sku,
        Brand = input.Brand,
        Price = input.Price,
        Quantity = input.Quantity,
        MinimumQuantity = input.MinimumQuantity,
        ProductCategoryId = input.ProductCategoryId,
        Tags = input.Tags,
        Color = input.Color,
        Size = input.Size,
        ExpiryDate = input.ExpiryDate,
        AddedById = int.Parse(id)
      };



      db.Products.Add(product);
      await db.SaveChangesAsync();

      return Results.CreatedAtRoute("GetProductById", new { Id = product.Id }, product);
    }).RequireAuthorization().WithName("CreateProduct");
    // update product
    group.MapPut("/", async (UpdateProductDTO input, DataContext db, IMapper mapper, ClaimsPrincipal claimsPrincipal) =>
    {
      var product = await db.Products.FindAsync(input.Id);

      if (product is null) return Results.NotFound("Invalid product id");

      var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (id is null) return Results.Unauthorized();

      HashSet<string> tags = [.. product.Tags];

      if (input.Tags.Count > 0)
      {
        foreach (string t in input.Tags)
        {
          tags.Add(t);
        }
      }

      product.Name = input.Name ?? product.Name;
      product.Sku = input?.Sku ?? product.Sku;
      product.Brand = input?.Brand ?? product.Brand;
      var slug = (product.Brand + " " + product.Name + " " + product.Sku).Slugify();
      product.Slug = slug;
      product.Tags = tags.ToList();
      product.Color = input?.Color ?? product?.Color;
      product.Size = input?.Size ?? product?.Size;
      product.Description = input?.Description ?? product.Description;
      product.ExpiryDate = input?.ExpiryDate ?? product.ExpiryDate;
      product.MinimumQuantity = input?.MinimumQuantity ?? product.MinimumQuantity;
      product.ProductCategoryId = input?.ProductCategoryId > 0 ? input.ProductCategoryId : product.ProductCategoryId;
      product.UpdatedById = int.Parse(id);
      product.UpdatedAt = DateTime.UtcNow;

      // mapper.Map(input, product);


      if (await db.SaveChangesAsync() > 0) return Results.NoContent();
      else return Results.BadRequest("Failed to save changes");

    }).RequireAuthorization().WithName("UpdateProduct");
    // get products
    group.MapGet("/", async (DataContext db, IMapper mapper) =>
    {
      return await db.Products.ProjectTo<GetProductsDTO>(mapper.ConfigurationProvider).ToListAsync();
    }).WithName("GetProducts");
    // get by product category products
    group.MapGet("/category/{id}", async (int id, DataContext db, IMapper mapper) =>
    {
      var products = await db.Products.ProjectTo<GetProductsDTO>(mapper.ConfigurationProvider).Where(p => p.ProductCategoryId == id).ToListAsync();


      return Results.Ok(products);
    }).WithName("GetProductsByCategory");
    // get product
    group.MapGet("/id/{id}", async (DataContext db, int id, IMapper mapper) =>
    {
      var product = await db.Products.ProjectTo<GetProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(p => p.Id == id);


      if (product is null) return Results.NotFound("Product not found");

      return Results.Ok(product);
    }).WithName("GetProductById");
    // delete product
    group.MapDelete("/id/{id}", async (DataContext db, int id) =>
    {

      var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);


      if (product is null) return Results.NotFound("Product not found");

      db.Products.Remove(product);

      await db.SaveChangesAsync();

      return Results.Ok(id);
    }).RequireAuthorization().WithName("DeleteProduct");
    return group;
  }
}
