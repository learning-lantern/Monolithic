using API.User.DTOs;

namespace API.Auth.DTOs
{
    /// <summary>
    /// Sign in response data transfare object class.
    /// </summary>
    public class SignInUserResponseDTO
    {
        public UserDTO? User;
        public string? Token { get; set; }

        public SignInUserResponseDTO() { }
        public SignInUserResponseDTO(UserDTO userDTO, string token)
        {
            User = new UserDTO(userDTO);
            Token = token;
        }
    }
}
