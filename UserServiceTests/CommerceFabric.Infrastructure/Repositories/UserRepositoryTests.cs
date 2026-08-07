using CommerceFabric.Core.Entities;
using CommerceFabric.Core.Enums;
using CommerceFabric.Infrastructure.DbContext;
using CommerceFabric.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
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
                    gender VARCHAR(50) NOT NULL,
                    bio VARCHAR(500)
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
                Gender = "Male",
                Bio = "Test user bio"
            };

            // Act
            var result = await _repository.CreateAsync(user);

            // Assert
            Assert.NotEqual(Guid.Empty, result.UserID);
            Assert.Equal(user.Gender, result.Gender);
            Assert.Equal(user.Bio, result.Bio);
        }

        [Fact]
        public async Task GetUserByUserIDAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserID = Guid.NewGuid(),
                Gender = "Female",
                Bio = "Test user bio"
            };

            await _repository.CreateAsync(user);

            // Act
            var result = await _repository.GetUserByUserIDAsync(user.UserID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result!.UserID);
            Assert.Equal(user.Gender, result.Gender);
            Assert.Equal(user.Bio, result.Bio);
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

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserID = Guid.NewGuid(),
                Gender = "Male",
                Bio = "Original bio"
            };

            await _repository.CreateAsync(user);

            user.Gender = "Female";
            user.Bio = "Updated bio";

            // Act
            var result = await _repository.UpdateAsync(user);

            // Assert returned user
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal("Female", result.Gender);
            Assert.Equal("Updated bio", result.Bio);

            // Assert database was actually updated
            var updatedUser = await _repository.GetUserByUserIDAsync(user.UserID);

            Assert.NotNull(updatedUser);
            Assert.Equal(user.UserID, updatedUser!.UserID);
            Assert.Equal("Female", updatedUser.Gender);
            Assert.Equal("Updated bio", updatedUser.Bio);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserID = Guid.NewGuid(),
                Gender = "Male",
                Bio = "Updated bio"
            };

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _repository.UpdateAsync(user));

            // Assert
            Assert.Equal(
                "Failed to update user in the database.",
                exception.Message);
        }
    }
}