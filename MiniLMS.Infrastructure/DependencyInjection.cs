using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Infrastructure.Persistence;
using MiniLMS.Infrastructure.Persistence.Repositories;


namespace MiniLMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext |  Repository + UoW |  Identity + JWT ...


            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }

    }
}
