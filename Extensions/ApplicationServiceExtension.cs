using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PartyFunApi.Repositories;

namespace PartyFunApi.Extensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddEndpointsApiExplorer();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // custom services
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<ICategoryRepo, CategoryRepository>();

    // Swagger
    services.AddSwaggerGen(option =>
     {
       option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
       {
         In = ParameterLocation.Header,
         Description = "Please enter your JWT Token",
         Name = "Authorization",
         Type = SecuritySchemeType.Http,
         BearerFormat = "JWT",
         Scheme = "bearer"
       }
       );

       option.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
              {
                new OpenApiSecurityScheme
                {
                  Reference = new OpenApiReference
                    {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                    }

                },

                Array.Empty<string>()

              }
         }
       );
     }
   );

    // JWT

    _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
              {
                string? TokenKey = configuration["JWT_SECRET"];
                if (TokenKey is null)
                {
                  Console.WriteLine("please add a TokenKey");
                  return;
                }
                options.TokenValidationParameters = new TokenValidationParameters
                {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)),
                  ValidateIssuer = false,
                  ValidateAudience = false,
                };
              }
            );


    return services;
  }
}
