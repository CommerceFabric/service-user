using Azure.Identity;
using CommerceFabric.Core.ServiceContracts;
using CommerceFabric.Core.Services;
using CommerceFabric.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace CommerceFabric.Core
{
    public static class DependencyInjection
    {

        /// <summary>
        /// Extension method to add core services to the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsersService, UsersService>();

            // Add Fluentvalidations to use as contract validators for the DTOs
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); // don't need to do this per validator, as it will automatically scan the assembly for all validators and register them in the DI container

            services.AddScoped<GraphServiceClient>(provider =>
            {
                // Client credentials for Microsoft Graph must request only Graph app scopes.
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var options = new ClientSecretCredentialOptions()
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };

                var clientSecret = configuration["AzureEntraAuth:ClientSecret"]!;
                clientSecret = clientSecret.Replace("$COMMERCEFABRIC_AZURE_ENTRA_CLIENT_SECRET", Environment.GetEnvironmentVariable("COMMERCEFABRIC_AZURE_ENTRA_CLIENT_SECRET") ?? "");


                // all 3 come from the custom 'CommerceFabric Backend' App Registration within the CommerceFabricWeb entra id auth tenant, which has the required Microsoft Graph API permissions granted to it
                var credential = new ClientSecretCredential(
                    tenantId: configuration["AzureEntraAuth:TenantId"]!,
                    clientId: configuration["AzureEntraAuth:ClientId"]!,
                    clientSecret: clientSecret,
                    options: options
                );

                return new GraphServiceClient(credential, scopes);
            });

            return services;
        }
    }
}
