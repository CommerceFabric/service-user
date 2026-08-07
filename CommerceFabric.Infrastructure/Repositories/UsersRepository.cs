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
            if (user.UserID == Guid.Empty) user.UserID = Guid.NewGuid();

            // execute the following SQL query to insert the user into the database using Dapper
            string query =
                "INSERT INTO public.users (userid, gender, bio) " +
                "VALUES (@UserID, @Gender, @Bio)";
            var rowsAffected = await _dbContext.Connection.ExecuteAsync(query, user);
            if(rowsAffected <= 0 ) throw new Exception("Failed to create user in the database.");

            return user;
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            // execute the following SQL query to update the user in the database using Dapper
            string query =
                "UPDATE public.users SET gender = @Gender, bio = @Bio WHERE userid = @UserID";
            var rowsAffected = await _dbContext.Connection.ExecuteAsync(query, user);
            if(rowsAffected <= 0 ) throw new Exception("Failed to update user in the database.");

            return user;
        }

        public async Task<ApplicationUser?> GetUserByUserIDAsync(Guid? userID)
        {
            // execute the following SQL query to get the user from the database using Dapper
            string query = "SELECT * FROM public.users WHERE userid = @UserID";
            var user = await _dbContext.Connection.QuerySingleOrDefaultAsync<ApplicationUser>(query, new { UserID = userID });

            return user;
        }
    }
}
