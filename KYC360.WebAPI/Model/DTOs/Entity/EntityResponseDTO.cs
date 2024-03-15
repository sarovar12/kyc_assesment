using KYC360.WebAPI.Model.DTOs.Address;
using KYC360.WebAPI.Model.DTOs.Date;
using KYC360.WebAPI.Model.DTOs.Name;


namespace KYC360.WebAPI.Model.DTOs.Entity
{
    public class EntityResponseDTO
    {
        public string Id {  get; set; }
        public bool Deceased { get; set; }
        public string? Gender { get; set; }
        public List<AddressResponseDTO>? Addresses { get; set; }
        public List<DateResponseDTO>? Dates { get; set; }
        public List<NameResponseDTO>? Names { get; set; }
    }
}
