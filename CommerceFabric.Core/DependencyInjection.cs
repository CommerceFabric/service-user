using CommerceFabric.Core.ServiceContracts;
using CommerceFabric.Core.Services;
using CommerceFabric.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceFabric.Core
{
    public static class DependencyInjection
    {

        /// <summary>
        /// Extension method to add core services to the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();

            // Add Fluentvalidations to use as contract validators for the DTOs
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); // don't need to do this per validator, as it will automatically scan the assembly for all validators and register them in the DI container

            return services;
        }
    }
}
