using AutoMapper;
using CommerceFabric.Core.DTOs;
using CommerceFabric.Core.Entities;
using CommerceFabric.Core.RepositoryContracts;
using CommerceFabric.Core.ServiceContracts;
using Microsoft.Kiota.Abstractions;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceFabric.Core.Services
{
    public class UsersService : IUsersService
    {
        #region Dependencies
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly GraphServiceClient _graphServiceClient;
        #endregion

        public UsersService(IUsersRepository usersRepository, IMapper mapper, GraphServiceClient graphServiceClient)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _graphServiceClient = graphServiceClient;
        }

        public async Task<UserDTO> GetUserByUserIDAsync(Guid? userID)
        {
            if(userID == null) return null;

            // Get user from Microsoft Graph API using the provided userID.
            // If Graph doesn't have this user ID, treat it as not found instead of 500.
            Microsoft.Graph.Models.User? user;
            try
            {
                user = await _graphServiceClient.Users[Convert.ToString(userID)].GetAsync();
            }
            catch (ApiException ex) when (ex.ResponseStatusCode == 404)
            {
                return null;
            }

            if (user == null) return null;

            // Call to postgres to get extra userDetails (eg gender) that aren't stored in Microsoft Azure Entra ID, but are stored in our own database
            // Completely optional and currently not editable via front-end (only by direct DB edits) just here for demonstration purposes, as an example of how to combine data from both Microsoft Graph API and our own database
            var userDetails = await _usersRepository.GetUserByUserIDAsync(userID);

            return new UserDTO(
                userID!.Value,
                user.Mail,
                user.DisplayName,
                userDetails?.Gender ?? "Unknown",
                user.Surname,
                userDetails?.Bio
            );
        }

        public async Task<bool> UpdateUserDetailsAsync(Guid? userID, UpdateUserDetailsRequest updateUserRequest)
        {
            // get the user details
            if (userID == null || updateUserRequest == null) return false;
            var userDetails = await _usersRepository.GetUserByUserIDAsync(userID);

            // if userDetails is null, create a new ApplicationUser entity
            if (userDetails == null)
            {
                userDetails = new ApplicationUser
                {
                    UserID = userID.Value,
                    Gender = updateUserRequest.Gender,
                    Bio = updateUserRequest.Bio

                };
                var createdUser = await _usersRepository.CreateAsync(userDetails);
                return createdUser != null;
            }

            // else, update the existing userDetails entity
            else
            {
                userDetails.Gender = updateUserRequest.Gender;
                userDetails.Bio = updateUserRequest.Bio;
                var updatedUser = await _usersRepository.UpdateAsync(userDetails);
                return updatedUser != null;
            }
        }
    }
}
