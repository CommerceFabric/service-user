using eCommerce.Core.Entities;
using eCommerce.Core.Enums;
using eCommerce.Core.RepositoryContracts;

namespace eCommerce.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
        {
            user.UserID = Guid.NewGuid(); // Generate a new GUID for the user being created

            // todo - actually save the user to the database

            return user;
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            // todo - actually get a user from the database based on email and password (currently just returning  a dummy user)
            return new ApplicationUser
            {
                UserID = Guid.NewGuid(),
                Email = email,
                Password = password,
                PersonName = "Dummy User",
                Gender = GenderOptions.Male.ToString(),
            };
        }
    }
}
