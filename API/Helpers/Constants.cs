namespace API.Helpers
{
    public static class Constants
    {
        public const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~])[a-zA-Z0-9 !\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]{6,}$";

        public const string NamePattern = "^((?![0-9!\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]).){2,30}$";
    }
}
