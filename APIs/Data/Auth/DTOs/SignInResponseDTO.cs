using APIs.Data.User.DTOs;

namespace APIs.Data.Auth.DTOs
{
    /// <summary>
    /// Sign In Response Object
    /// </summary>
    public class SignInResponseDTO
    {
        public UserDTO? User;
        public string? Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SignInResponseDTO()
        {
            User = null;
            Token = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="token"></param>
        public SignInResponseDTO(UserDTO userDTO, string token)
        {
            User = new UserDTO(userDTO);
            Token = token;
        }
    }
}
