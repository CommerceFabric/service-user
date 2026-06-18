using Dapper;
using CommerceFabric.Core.Entities;
using CommerceFabric.Core.Enums;
using CommerceFabric.Core.RepositoryContracts;
using CommerceFabric.Infrastructure.DbContext;

namespace CommerceFabric.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperDbContext _dbContext;

        public UsersRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
        {
            // todo - not live code as it is insecure - this will be replaced with an Azure AD B2C authentication system, this is just a temporary measure to get the project working with a simple login system.
            user.UserID = Guid.NewGuid(); // Generate a new GUID for the user being created

            // execute the following SQL query to insert the user into the database using Dapper
            string query =
                "INSERT INTO public.users (userid, email, password, personname, gender) " +
                "VALUES (@UserID, @Email, @Password, @PersonName, @Gender)";
            var rowsAffected = await _dbContext.Connection.ExecuteAsync(query, user);
            if(rowsAffected <= 0 ) throw new Exception("Failed to create user in the database.");

            return user;
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            // todo - not live code as it is insecure - this will be replaced with an Azure AD B2C authentication system, this is just a temporary measure to get the project working with a simple login system.

            // execute the following SQL query to get the user from the database using Dapper
            string query = "SELECT * FROM public.users WHERE email = @Email AND password = @Password";
            var user = await _dbContext.Connection.QuerySingleOrDefaultAsync<ApplicationUser>(query, new { Email = email, Password = password });

            return user;
        }
    }
}
