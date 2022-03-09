using API.Data.DTOs;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories.Auth
{
    /// <summary>
    /// Auth repository interface for auth repository class to applay the dependency injection.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Creates the specified user in the backing store with given password, as an asynchronous operation.
        /// </summary>
        /// <param name="createUserDTO"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IdentityResult of the operation.
        /// </returns>
        public Task<IdentityResult> CreateAsync(CreateUserDTO createUserDTO);

        /// <summary>
        /// Attempts to sign in the specified userName and password combination as an asynchronous operation.
        /// </summary>
        /// <param name="signInUserDTO"></param>
        /// <returns>
        /// The signed in user and its JWT token (object of sign in response data transfare class).
        /// </returns>
        public Task<SignInUserResponseDTO> SignInAsync(SignInUserDTO signInUserDTO);

        /// <summary>
        /// Sends confirmation email token to the email.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IdentityResult of the operation.
        /// </returns>
        public Task<IdentityResult> SendConfirmationEmailAsync(string userEmail);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        /// Task that represents the asynchronous operation, containing IdentityResult of the operation.
        /// </returns>
        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}
