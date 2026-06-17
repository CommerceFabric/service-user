using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.Services
{
    public class UsersService : IUsersService
    {
        #region Dependencies
        private readonly IUsersRepository _usersRepository;
        #endregion

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
        {
            // todo - this is insecure, just a simple placeholder for now, will later be replaced with Azure AD B2C authentication
            var user = await _usersRepository.GetUserByEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

            if (user == null) return null;

            return new AuthenticationResponse(
                user.UserID,
                user.Email,
                user.PersonName,
                user.Gender,
                "fake_token",
                true
            );
        }

        public async Task<AuthenticationResponse> Register(RegisterRequest registerRequest)
        {
            // todo - this is insecure, just a simple placeholder for now, will later be replaced with Azure AD B2C authentication

            var user = new ApplicationUser
            {
                UserID = Guid.NewGuid(),
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                Gender = registerRequest.Gender.ToString()
            };

            var registeredUser = await _usersRepository.CreateAsync(user);

            if(registeredUser == null) return null;

            return new AuthenticationResponse(
                registeredUser.UserID,
                registeredUser.Email,
                registeredUser.PersonName,
                registeredUser.Gender,
                "fake_token",
                true
            );
        }
    }
}
