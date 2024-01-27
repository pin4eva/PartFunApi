using System.Net;
using System.Text.Json;

namespace PartyFunApi.Middlewares;

public class ExceptionMiddlewares(RequestDelegate next, ILogger<ExceptionMiddlewares> logger, IHostEnvironment env)
{
  private readonly RequestDelegate next = next;
  public ILogger<ExceptionMiddlewares> Logger { get; } = logger;
  private readonly IHostEnvironment env = env;

  public async Task InvokeAsync(HttpContext httpContext)
  {

    try
    {
      await next(httpContext);
    }
    catch (Exception ex)
    {
      string errorMessage = "Internal Server Error";

      if (!string.IsNullOrEmpty(ex?.Message))
      {
        errorMessage = ex.Message;
      }
      Logger.LogError(ex, errorMessage);
      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      var response = env.IsDevelopment()
            ? new ApiExceptions(httpContext.Response.StatusCode, errorMessage, ex?.StackTrace?.ToString() ?? "")
            : new ApiExceptions(httpContext.Response.StatusCode, errorMessage, "Internal Server Error");

      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

      var json = JsonSerializer.Serialize(response, options);

      await httpContext.Response.WriteAsync(json);
    }
  }
}
