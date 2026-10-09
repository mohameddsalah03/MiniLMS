using Microsoft.AspNetCore.Mvc;
using MiniLMS.API.Models;

namespace MiniLMS.API.Extensions;

public static class ApiServiceExtensions // this class to make response to frontend in one pattern 
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(entry => entry.Value?.Errors.Count > 0)
                        .ToDictionary(
                            entry => entry.Key,
                            entry => entry.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                    return new BadRequestObjectResult(BaseResponse<object>.Fail("Validation failed.", errors));
                };
            });

        return services;
    }
}