using CommerceFabric.Core.Entities;

namespace CommerceFabric.Core.RepositoryContracts
{
    /// <summary>
    /// Contract to define the operations related to user management in the database
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApplicationUser> CreateAsync(ApplicationUser user);

        /// <summary>
        /// Retrieves a user from the database based on the provided email and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string email, string password); // todo - this is insecure, will soon be replaced with Azure AD B2C auth, just here as a temporary measure to get the project working with a simple login system.


    }
}
