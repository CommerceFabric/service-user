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
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApplicationUser> UpdateAsync(ApplicationUser user);

        /// <summary>
        /// Retrieves a user from the database based on the provided user ID.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<ApplicationUser?> GetUserByUserIDAsync(Guid? userID);
    }
}
