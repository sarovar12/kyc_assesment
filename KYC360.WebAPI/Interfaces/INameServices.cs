
using KYC360.WebAPI.Model.DTOs.Name;

namespace KYC360.WebAPI.Interfaces
{
    public interface INameServices
    {
        public Task<bool> AddNames(List<NameRequestDTO> names, string entityId);
    }
}
