using KYC360.WebAPI.Model.DTOs.Address;

namespace KYC360.WebAPI.Interfaces
{
    public interface IAddressServices
    {
        Task<bool> AddAddresses(List<AddressRequestDTO> addresses, string entityId);

    }
}
