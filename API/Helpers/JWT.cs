using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Helpers
{
    public static class JWT
    {
        public const string ValidIssuer = "https://localhost:5001";
        public const string ValidAudience = "https://localhost:5001";

        public static readonly SymmetricSecurityKey IssuerSigningKey = new(key: Encoding.UTF8.GetBytes("nDzMuunoP6JbalKjeyccXU3xUeMrQsVN"));
    }
}
