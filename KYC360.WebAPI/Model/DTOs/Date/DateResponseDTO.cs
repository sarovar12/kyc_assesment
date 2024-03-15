namespace KYC360.WebAPI.Model.DTOs.Date
{
    public class DateResponseDTO
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string? DateType { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
