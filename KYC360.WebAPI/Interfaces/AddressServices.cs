using AutoMapper;
using KYC360.WebAPI.DatabaseContext;
using KYC360.WebAPI.Model.DTOs.Address;
using KYC360.WebAPI.Model.Entities;
using Serilog;


namespace KYC360.WebAPI.Interfaces
{
    public class AddressServices : IAddressServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public AddressServices(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> AddAddresses(List<AddressRequestDTO> addresses, string entityId)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var addressDto in addresses)
                    {
                        var address = _mapper.Map<Address>(addressDto);
                        address.EntityId = entityId;
                        await _db.AddAsync(address);
                    }

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true; 
                }
                catch (Exception ex)
                {
                    Log.Error("Failed to add address, Error:", ex);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
