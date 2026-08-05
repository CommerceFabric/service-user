using CommerceFabric.Core.Entities;
using CommerceFabric.Core.Enums;
using CommerceFabric.Infrastructure.DbContext;
using CommerceFabric.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Reflection;
using Testcontainers.PostgreSql;
using Xunit;

namespace CommerceFabric.Infrastructure.Tests.Repositories
{
    public class UsersRepositoryTests : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgresContainer;

        private DapperDbContext _dbContext = null!;
        private UsersRepository _repository = null!;

        public UsersRepositoryTests()
        {
            _postgresContainer = new PostgreSqlBuilder()
                .WithDatabase("testdb")
                .WithUsername("testuser")
                .WithPassword("testpassword")
                .Build();
        }

        public async Task InitializeAsync()
        {
            // Start PostgreSQL container
            await _postgresContainer.StartAsync();

            // Create configuration containing the test database connection string
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {
                        "ConnectionStrings:DefaultConnection",
                        _postgresContainer.GetConnectionString()
                    }
                })
                .Build();

            // Create the application's Dapper DbContext
            _dbContext = new DapperDbContext(configuration);

            // Create repository
            _repository = new UsersRepository(_dbContext);

            // Create the users table
            await using var connection = new NpgsqlConnection(
                _postgresContainer.GetConnectionString());

            await connection.OpenAsync();

            await using var command = connection.CreateCommand();

            command.CommandText = """
                CREATE TABLE public.users
                (
                    userid UUID PRIMARY KEY,
                    email TEXT NOT NULL,
                    password TEXT NOT NULL,
                    personname TEXT NOT NULL,
                    gender TEXT NOT NULL
                );
                """;

            await command.ExecuteNonQueryAsync();
        }

        public async Task DisposeAsync()
        {
            await _postgresContainer.DisposeAsync();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                Password = "password123",
                PersonName = "Test User",
                Gender = "Male"
            };

            // Act
            var result = await _repository.CreateAsync(user);

            // Assert
            Assert.NotEqual(Guid.Empty, result.UserID);
            Assert.Equal("test@example.com", result.Email);
            Assert.Equal("password123", result.Password);
            Assert.Equal("Test User", result.PersonName);
            Assert.Equal("Male", result.Gender);
        }

        [Fact]
        public async Task GetUserByEmailAndPasswordAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                Password = "password123",
                PersonName = "Test User",
                Gender = "Male"
            };

            await _repository.CreateAsync(user);

            // Act
            var result = await _repository.GetUserByEmailAndPasswordAsync(
                "test@example.com",
                "password123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.PersonName, result.PersonName);
        }

        [Fact]
        public async Task GetUserByEmailAndPasswordAsync_ShouldReturnNull_WhenCredentialsAreIncorrect()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                Password = "password123",
                PersonName = "Test User",
                Gender = "Male"
            };

            await _repository.CreateAsync(user);

            // Act
            var result = await _repository.GetUserByEmailAndPasswordAsync(
                "test@example.com",
                "wrongpassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByUserIDAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                Password = "password123",
                PersonName = "Test User",
                Gender = "Male"
            };

            await _repository.CreateAsync(user);

            // Act
            var result = await _repository.GetUserByUserIDAsync(user.UserID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.PersonName, result.PersonName);
        }

        [Fact]
        public async Task GetUserByUserIDAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userID = Guid.NewGuid();

            // Act
            var result = await _repository.GetUserByUserIDAsync(userID);

            // Assert
            Assert.Null(result);
        }
    }
}