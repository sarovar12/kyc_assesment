namespace KYC360.WebAPI.Model.DTOs.Address
{
    public class AddressResponseDTO
    {
        public string Id { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string EntityId { get; set; }
    }
}
