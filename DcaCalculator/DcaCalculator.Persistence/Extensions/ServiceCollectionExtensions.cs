using DcaCalculator.Application.Interfaces.Repositories;
using DcaCalculator.Persistence.Contexts;
using DcaCalculator.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DcaCalculator.Application.Interfaces;
using DcaCalculator.Persistence.HttpClients;

namespace DcaCalculator.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>()
                .AddTransient<ICoinmarketCapClient, CoinmarketCapClient>();
        }
    }
}
