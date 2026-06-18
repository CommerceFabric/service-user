using CommerceFabric.Core.RepositoryContracts;
using CommerceFabric.Infrastructure.DbContext;
using CommerceFabric.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceFabric.Infrastructure
{
    public static class DependencyInjection
    {

        /// <summary>
        /// Extension method to add infrastructure services to the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<DapperDbContext>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            return services;
        }
    }
}
