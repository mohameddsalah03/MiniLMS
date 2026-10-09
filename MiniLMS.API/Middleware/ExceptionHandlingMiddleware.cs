using MiniLMS.API.Models;
using MiniLMS.Application.Common.Exceptions;

namespace MiniLMS.API.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                logger.LogWarning("The response has already started, so the exception cannot be written.");
                throw;
            }

            var (statusCode, message, errors) = MapException(ex);

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                logger.LogError(ex, "Unhandled exception for {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                if (environment.IsDevelopment())
                    message = ex.Message;
            }
            else
            {
                logger.LogWarning("Handled {ExceptionType} => {StatusCode}: {Message}",
                    ex.GetType().Name, statusCode, ex.Message);
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(BaseResponse<object>.Fail(message, errors));
        }
    }

    private static (int StatusCode, string Message, IDictionary<string, string[]>? Errors) MapException(Exception ex)
        => ex switch
        {
            ValidationException validation => (StatusCodes.Status400BadRequest, validation.Message, validation.Errors),
            BadRequestException => (StatusCodes.Status400BadRequest, ex.Message, null),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, ex.Message, null),
            ForbiddenException => (StatusCodes.Status403Forbidden, ex.Message, null),
            NotFoundException => (StatusCodes.Status404NotFound, ex.Message, null),
            ConflictException => (StatusCodes.Status409Conflict, ex.Message, null),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.", null)
        };
}