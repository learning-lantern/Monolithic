using API.Auth.DTOs;
using API.User.DTOs;
using API.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> CreateAsync(CreateUserDTO createUserDTO)
        {
            var user = new UserModel()
            {
                UserName = createUserDTO.Email,
                Email = createUserDTO.Email,
                FirstName = createUserDTO.FirstName.Trim(),
                LastName = createUserDTO.LastName.Trim()
            };

            var createAsyncResult = await userManager.CreateAsync(user, createUserDTO.Password);

            return createAsyncResult.Succeeded ?
                await SendConfirmationEmailAsync(user.Email) : createAsyncResult;
        }

        public async Task<SignInUserResponseDTO> SignInAsync(SignInUserDTO signInUserDTO)
        {
            var passwordSignInAsyncResult = await signInManager.PasswordSignInAsync(
                userName: signInUserDTO.Email, password: signInUserDTO.Password,
                isPersistent: true, lockoutOnFailure: false);

            if (!passwordSignInAsyncResult.Succeeded)
            {
                return new SignInUserResponseDTO();
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: signInUserDTO.Email),
                new Claim(type: "university", value: signInUserDTO.University),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
            };

            var user = await userManager.FindByEmailAsync(signInUserDTO.Email);
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(type: ClaimTypes.Role, value: role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(configuration["JWT:IssuerSigningKey"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key: symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
                );

            return new SignInUserResponseDTO(new UserDTO(user),
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
                        Code = "NotFound",
                        Description = "There is no user in this University with this email."
                    });
                }

                var token = HttpUtility.UrlEncode(
                    await userManager.GenerateEmailConfirmationTokenAsync(user));

                var messageBody = $"<h1>Welcome To Learning Lantern</h1><br><p> Thanks for registering at learning lantern please click <strong><a href=\"https://localhost:5001/api/Auth/ConfirmEmail?userId={user.Id}&token={token}\" target=\"_blank\">here</a></strong> to activate your account</p>";

                var smtpClient = new SmtpClient(host: configuration["SMTP:Host"], port: 587)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                        userName: configuration["SMTP:Credentials:UserName"],
                        password: configuration["SMTP:Credentials:Password"])
                };

                var mailMessage = new MailMessage(from: configuration["SMTP:Credentials:UserName"], to: userEmail, subject: "Confirmation Email", body: messageBody)
                {
                    IsBodyHtml = true
                };

                await smtpClient.SendMailAsync(mailMessage);

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "ConfirmFailure",
                    Description = "Send Confirmation Email Failure"
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
                    Code = "NotFound",
                    Description = "There is no user in this University with this Id."
                });
            }

            return await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
