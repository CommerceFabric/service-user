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
        /// Defines a method for user login that takes a LoginRequest object as input and returns an AuthenticationResponse object. 
        /// The method is responsible for authenticating the user based on the provided email and password, and generating a JWT token if the authentication is successful.
        /// </summary>
        /// <param name="loginRequest">The login request containing the user's email and password.</param>
        /// <returns>An AuthenticationResponse object containing the authentication result and JWT token if successful.</returns>
        Task<AuthenticationResponse> Login(LoginRequest loginRequest);

        /// <summary>
        /// Defines a method for user registration that takes a LoginRequest object as input and returns an AuthenticationResponse object.
        /// The method is responsible for registering a new user based on the provided email and password, and generating a JWT token if the registration is successful.
        /// </summary>
        /// <param name="registerRequest">The registration request containing the user's email and password.</param>
        /// <returns>An AuthenticationResponse object containing the registration result and JWT token if successful.</returns>
        Task<AuthenticationResponse> Register(RegisterRequest registerRequest);
    }
}
