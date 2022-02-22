namespace APIs.Data.Auth.DTOs
{
    public class ConfirmEmailDTO
    {
        public string Id { get; set; }

        public ConfirmEmailDTO(string id)
        {
            Id = id;
        }
    }
}
