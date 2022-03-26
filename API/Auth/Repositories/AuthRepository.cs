using API.Auth.DTOs;
using API.Helpers;
using API.User.DTOs;
using API.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace API.Auth.Repositories
{
    /// <summary>
    /// Auth repository class for authentication services, implements the IAuthRepository interface.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;

        public AuthRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(CreateDTO createDTO)
        {
            var user = new UserModel()
            {
                UserName = createDTO.Email,
                Email = createDTO.Email,
                FirstName = createDTO.FirstName.Trim(),
                LastName = createDTO.LastName.Trim()
            };

            var createAsyncResult = await userManager.CreateAsync(user, createDTO.Password);

            return createAsyncResult.Succeeded ?
                await SendConfirmationEmailAsync(user.Email) : createAsyncResult;
        }

        public async Task<SignInResponseDTO> SignInAsync(SignInDTO signInDTO)
        {
            var passwordSignInAsyncResult = await signInManager.PasswordSignInAsync(
                userName: signInDTO.Email, password: signInDTO.Password,
                isPersistent: true, lockoutOnFailure: false);

            if (!passwordSignInAsyncResult.Succeeded)
            {
                return new SignInResponseDTO();
            }

            var user = await userManager.FindByEmailAsync(signInDTO.Email);
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: signInDTO.Email),
                new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Id),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(type: ClaimTypes.Role, value: role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(JWT.IssuerSigningKey));

            var token = new JwtSecurityToken(
                issuer: JWT.ValidIssuer,
                audience: JWT.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(key: symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
                );

            return new SignInResponseDTO(new UserDTO(user),
                new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<IdentityResult> SendConfirmationEmailAsync(string userEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError()
                    {
                        Code = StatusCodes.Status404NotFound.ToString(),
                        Description = Message.UserEmailNotFound
                    });
                }

                var token = HttpUtility.UrlEncode(await userManager.GenerateEmailConfirmationTokenAsync(user));

                var mailMessage = new MailMessage(from: Message.FromEmail, to: userEmail, subject: Message.EmailSubject, body: Message.EmailBody(user.Id, token))
                {
                    IsBodyHtml = true
                };

                await Helper.smtpClient.SendMailAsync(mailMessage);

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "ConfirmationFailure",
                    Description = ex.Message
                });
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = StatusCodes.Status404NotFound.ToString(),
                    Description = Message.UserIdNotFound
                });
            }

            return await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
