using CommerceFabric.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceFabric.Core.ServiceContracts
{
    /// <summary>
    /// Contract for users service that contains methods for the users' use cases
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Defines a method for retrieving user information based on the provided user ID.
        /// The method is responsible for fetching the user details from the data source and returning a User DTO object containing the user's information.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A UserDTO object containing the user's information, or null if the user is not found.</returns>
        Task<UserDTO> GetUserByUserIDAsync(Guid? userID);

        /// <summary>
        /// Defines a method for updating user information based on the provided user ID and update request.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="updateUserRequest">A UpdateUserDetailsRequest object containing the updated user information.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<bool> UpdateUserDetailsAsync(Guid? userID, UpdateUserDetailsRequest updateUserRequest);

    }
}
