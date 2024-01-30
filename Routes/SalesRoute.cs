using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.DTO;
using PartyFunApi.Model;

namespace PartyFunApi.Routes;

public static class SalesRoute
{
  public static RouteGroupBuilder MapSalesRoute(this IEndpointRouteBuilder routeBuilder)
  {

    var group = routeBuilder.MapGroup("api/v1/sales").WithParameterValidation().WithTags("Sales").RequireAuthorization().WithOpenApi();

    // get sales
    group.MapGet("/", async (DataContext db, IMapper mapper) =>
    {
      var sales = await db.Sales.ProjectTo<GetAllSalesDTO>(mapper.ConfigurationProvider).ToListAsync();

      return sales;
    }).WithName("GetAllSales");

    // get sales
    group.MapGet("/id/{id}", async (DataContext db, IMapper mapper, int id) =>
        {
          var sales = await db.Sales.ProjectTo<GetSingleSalesDTO>(mapper.ConfigurationProvider).Where(s => s.Id == id).SingleOrDefaultAsync();
          if (sales is null) return Results.NotFound($"Sales with id {id} not found");
          return Results.Ok(sales);
        }).WithName("GetSalesById");

    // Post single sale
    group.MapPost("/", async (DataContext db, IMapper mapper, CreateSalesDTO input, ClaimsPrincipal claimsPrincipal) =>
          {


            var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id is null) return Results.Unauthorized();

            Sales sales = new()
            {
              PaymentMethod = input.PaymentMethod ?? "Cash",
              ProductId = input.ProductId,
              Quantity = input.Quantity,
              TotalAmount = input.TotalAmount,
              UnitPrice = input.UnitPrice,
              UpdatedAt = DateTime.UtcNow,
              CreatedAt = DateTime.UtcNow,
              CashierId = int.Parse(id),
              Discount = input.Discount
            };

            db.Sales.Add(sales);

            await db.SaveChangesAsync();

            return Results.CreatedAtRoute("GetSalesById", new { Id = sales.Id }, sales);

          }).WithName("CreateSingleSale");

    group.MapPost("/bulk", async (DataContext db, IMapper mapper, [FromBody] CreateBulkSalesDTO inputList, ClaimsPrincipal claimsPrincipal) =>
              {


                var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (id is null) return Results.Unauthorized();

                List<Sales> allSales = [];
                var inputs = inputList.Sales;
                var lastSales = await db.Sales.OrderByDescending(s => s.CreatedAt).FirstOrDefaultAsync();
                int invoiceNumber = !string.IsNullOrEmpty(lastSales?.InvoiceNo) ? int.Parse(lastSales.InvoiceNo) : 1;

                foreach (CreateSalesDTO input in inputs)
                {
                  Sales sales = new()
                  {
                    PaymentMethod = input.PaymentMethod ?? "Cash",
                    ProductId = input.ProductId,
                    Quantity = input.Quantity,
                    TotalAmount = input.TotalAmount,
                    UnitPrice = input.UnitPrice,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    CashierId = int.Parse(id),
                    Discount = input.Discount,
                    InvoiceNo = (invoiceNumber + 1).ToString().PadLeft(7, '0')

                  };

                  db.Sales.Add(sales);
                  allSales.Add(sales);
                }



                await db.SaveChangesAsync();

                return Results.Ok(allSales);

              }).WithName("CreateBulkSales");


    // update sales
    group.MapPut("/", async (DataContext db, IMapper mapper, UpdateSalesDTO input) =>
    {
      var sales = await db.Sales.ProjectTo<GetSingleSalesDTO>(mapper.ConfigurationProvider).Where(s => s.Id == input.Id).SingleOrDefaultAsync();
      if (sales is null) return Results.NotFound($"Sales with id {input.Id} not found");

      mapper.Map(input, sales);

      await db.SaveChangesAsync();
      return Results.NoContent();

    }).WithName("UpdateSales");

    // delete sales
    group.MapDelete("/id/{id}", async (DataContext db, IMapper mapper, int id) =>
           {
             var sales = await db.Sales.Where(s => s.Id == id).SingleOrDefaultAsync();
             if (sales is null) return Results.NotFound($"Sales with id {id} not found");

             db.Sales.Remove(sales);
             await db.SaveChangesAsync();
             return Results.Ok(sales);
           }).WithName("DeleteSalesById");
    return group;
  }


}
