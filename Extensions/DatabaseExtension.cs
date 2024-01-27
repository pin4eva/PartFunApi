
using Microsoft.EntityFrameworkCore;
using PartyFunApi.Data;

namespace PartyFunApi.Extensions;

public static class DatabaseExtension
{
  public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddDbContext<DataContext>(option =>
   {

     string DB_HOST = configuration["DATABASE_HOST"] ?? "localhost";
     string? DB_USERNAME = configuration["DATABASE_USERNAME"];
     string? DB_NAME = configuration["DATABASE_NAME"];
     string? DB_PASSWORD = configuration["DATABASE_PASSWORD"];
     string? DB_PORT = configuration["DATABASE_PORT"];
     string? JWT_SECRET = configuration["JWT_SECRET"];

     // "Host=localhost; Database=joint_heirs; Username=postgres; Password=peter;"
     string dbUrl = $"Host={DB_HOST}; Port={DB_PORT}; Database={DB_NAME}; Username={DB_USERNAME}; Password={DB_PASSWORD} ";


     _ = option.UseNpgsql(dbUrl);
   }
   );
    return services;
  }
}
