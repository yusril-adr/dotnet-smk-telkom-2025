using System.Net;
using System.Net.Mime;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;

namespace dotnet_smk_telkom_2025.Infrastructure.Middlewares;

public class HandlerException(
    RequestDelegate next,
    IConfiguration config
)
{
    private readonly RequestDelegate _next = next;
    private readonly IConfiguration _config = config;


    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var isInDebugMode = bool.Parse(_config["App:Debug"]);
            var stackTrace = isInDebugMode ? error.StackTrace?.Trim() : null;

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errorMessage = error.Message;
            if (error is AppException appException)
            {
                statusCode = appException.StatusCode;
                errorMessage = appException.Message;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsJsonAsync(new { errorMessage, stackTrace, statusCode });
        }
    }
}
