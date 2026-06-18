using AutoMapper;
using CommerceFabric.Core.DTOs;
using CommerceFabric.Core.Entities;
using CommerceFabric.Core.RepositoryContracts;
using CommerceFabric.Core.ServiceContracts;
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
        #endregion

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
        {
            // todo - this is insecure, just a simple placeholder for now, will later be replaced with Azure AD B2C authentication
            var user = await _usersRepository.GetUserByEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

            if (user == null) return null;

            return _mapper.Map<AuthenticationResponse>(user) with
            {
                Success = true,
                Token = "dummy-jwt"
            };
        }

        public async Task<AuthenticationResponse> Register(RegisterRequest registerRequest)
        {
            // todo - this is insecure, just a simple placeholder for now, will later be replaced with Azure AD B2C authentication

            var user = _mapper.Map<ApplicationUser>(registerRequest);
            user.UserID = Guid.NewGuid(); // Generate a new GUID for the user being created

            var registeredUser = await _usersRepository.CreateAsync(user);

            if(registeredUser == null) return null;

            return _mapper.Map<AuthenticationResponse>(registeredUser) with
            {
                Success = true,
                Token = "dummy-jwt"
            };
        }
    }
}
