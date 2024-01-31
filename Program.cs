using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;
using PartyFunApi.Extensions;
using PartyFunApi.Helpers;
using PartyFunApi.Middlewares;
using PartyFunApi.Routes;


// Load .env on development
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
Dotenv.Load(dotenv);

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();



builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddDatabaseService(builder.Configuration);

var app = builder.Build();

// auto apply migration on service start
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();

}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger?.LogError(ex, "An error occured during migration");
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.MapGet("/", ([FromHeader] HttpContext context, [FromServices] IAntiforgery antiforgery) =>
// {
//     var token = antiforgery.GetAndStoreTokens(context);

//     if (token?.HeaderName != null && token?.RequestToken != null)
//     {
//         context.Response.Cookies.Append("X-XSRF-TOKEN", token.RequestToken, new CookieOptions { HttpOnly = false });
//     }
//     Console.WriteLine("tokenHeaderName {0}, tokenHeaderCookier {1}, tokenHeaderRequestToken {2}", token.HeaderName, token.CookieToken, token.RequestToken);
//     return Results.NoContent();
// });

app.MapUserRoute();
app.MapProductRoute();
app.MapSalesRoute();
// app.MapProductRoute();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder =>
builder.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://*.vercel.app", "https://*.jointheirsassesmbly.org")
    .AllowCredentials()
    .AllowAnyHeader()
);

app.UseMiddleware<ExceptionMiddlewares>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();





app.Run();
