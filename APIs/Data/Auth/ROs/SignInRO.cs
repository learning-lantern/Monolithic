namespace APIs.Data.Auth.ROs
{
    /// <summary>
    /// Sign In Return Object
    /// </summary>
    public class SignInRO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Token { get; set; }

        public SignInRO(string id, string firstName, string lastName, string? token)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Token = token;
        }
    }
}
