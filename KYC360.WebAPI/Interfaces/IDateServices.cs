using KYC360.WebAPI.Model.DTOs.Date;

namespace KYC360.WebAPI.Interfaces
{
    public interface IDateServices
    {
        Task<bool> AddDates(List<DateRequestDTO> dates, string entityId);
    }
}
