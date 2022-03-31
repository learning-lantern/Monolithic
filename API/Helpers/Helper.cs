﻿using System.Text.RegularExpressions;

namespace API.Helpers
{
    public static class Helper
    {
        public static readonly Regex PasswordRegex = new(Pattern.Password);

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
