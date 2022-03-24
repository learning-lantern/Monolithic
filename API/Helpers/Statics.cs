using System.Text.RegularExpressions;

namespace API.Helpers
{
    public static class Statics
    {
        public static Regex PasswordRegex = new(Constants.PasswordPattern);
    }
}
