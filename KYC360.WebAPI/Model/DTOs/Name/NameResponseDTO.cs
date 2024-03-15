namespace KYC360.WebAPI.Model.DTOs.Name
{
    public class NameResponseDTO
    {
        public string EntityId { get; set; }

        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Surname { get; set; }
    }
}
