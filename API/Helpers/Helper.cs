using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace API.Helpers
{
    public static class Helper
    {
        public static readonly Regex PasswordRegex = new(Pattern.Password);

        public static readonly SmtpClient smtpClient = new(host: Message.FromEmailHost, port: 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(
                userName: Message.FromEmail,
                password: Message.FromEmailPassword)
        };

        private static readonly List<string> universities = new()
        {
            "Assiut University"
        };

        public static bool IsUniversityValid(string universityName)
        {
            foreach (var university in universities)
            {
                if (university == universityName)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsNameValid(string name) => name.Replace(" ", "").Length >= 2;
    }
}
