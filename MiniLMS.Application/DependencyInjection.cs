using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniLMS.Application.Common.Behaviors;

namespace MiniLMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //  MediatR + FluentValidation + ValidationBehavior

        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // for fleunt validation 
        });

        services.AddValidatorsFromAssembly(assembly);

        // 
        services.AddAutoMapper(config => config.AddMaps(assembly));


        return services;
    }
}