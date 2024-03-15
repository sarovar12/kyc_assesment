using KYC360.WebAPI.Model.DTOs.Address;
using KYC360.WebAPI.Model.DTOs.Date;
using KYC360.WebAPI.Model.DTOs.Name;

namespace KYC360.WebAPI.Model.DTOs.Entity
{
    public class EntityRequestDTO
    {
        public bool Deceased { get; set; }
        public string? Gender { get; set; }
        public List<AddressRequestDTO>? Addresses { get; set; }
        public List<DateRequestDTO>? Dates { get; set; }
        public List<NameRequestDTO>? Names { get; set; }
    }
}
