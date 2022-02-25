﻿using System.ComponentModel.DataAnnotations;

namespace APIs.Data.Auth.DTOs
{
    /// <summary>
    /// Sign Up data transfare object class, inherits from "SignInDTO" class.
    /// </summary>
    public class CreateDTO : SignInDTO
    {
        [Required, StringLength(30), RegularExpression("^((?![0-9 !\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]).){2,30}$")]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30), RegularExpression("^((?![0-9 !\"#$%&'()*+,-./\\:;<=>?@[" + @"\]" + "^_`{|}~]).){2,30}$")]
        public string LastName { get; set; } = null!;
    }
}
