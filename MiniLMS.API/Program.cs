using MiniLMS.API.Extensions;
using MiniLMS.API.Middleware;
using MiniLMS.Application;
using MiniLMS.Infrastructure;
using MiniLMS.Infrastructure.Persistence.Seeding;

namespace MiniLMS.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApiServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region register Service Layers

            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            #endregion

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
                await app.Services.SeedDatabaseAsync();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}