using Microsoft.Extensions.DependencyInjection;

namespace MiniLMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //  MediatR + FluentValidation + ValidationBehavior
        return services;
    }
}