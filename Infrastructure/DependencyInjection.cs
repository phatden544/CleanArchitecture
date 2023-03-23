using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<Persistence.ApplicationDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RijsatDatabase"),
                b => b.MigrationsAssembly(typeof(Persistence.ApplicationDBContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped((Func<IServiceProvider, Application.Common.Interfaces.IApplicationDBContext>)(provider => provider.GetService<Persistence.ApplicationDBContext>()));
            return services;
        }
    }
}