using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CommerceFabric.Infrastructure.DbContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public IDbConnection Connection => _connection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            connectionString = connectionString.Replace("$COMMERCEFABRIC_USERSERVICE_DB_HOST", Environment.GetEnvironmentVariable("COMMERCEFABRIC_USERSERVICE_DB_HOST") ?? "localhost");
            connectionString = connectionString.Replace("$COMMERCEFABRIC_USERSERVICE_DB_PASSWORD", Environment.GetEnvironmentVariable("COMMERCEFABRIC_USERSERVICE_DB_PASSWORD") ?? "admin");

            if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Create a new NpgsqlConnection using the connection string from the configuration
            _connection = new NpgsqlConnection(connectionString);
        }

    }
}
