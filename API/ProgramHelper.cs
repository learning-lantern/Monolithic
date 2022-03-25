using System.Text.RegularExpressions;

namespace API
{
    public static class ProgramHelper
    {
        public const string NamePattern = "^((?![0-9!\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]).){2,30}$";
        public const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~])[a-zA-Z0-9 !\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]{6,}$";

        public static readonly Regex PasswordRegex = new(PasswordPattern);
    }
}
