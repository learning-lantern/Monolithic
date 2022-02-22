namespace APIs.Data.Auth.DTOs
{
    public class CreateDTO
    {
        public string UserName { get; set; }

        public CreateDTO(string userName)
        {
            UserName = userName;
        }
    }
}
