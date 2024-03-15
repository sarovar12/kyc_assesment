using AutoMapper;
using KYC360.WebAPI.DatabaseContext;
using KYC360.WebAPI.Model.DTOs.Name;
using KYC360.WebAPI.Model.Entities;
using Serilog;


namespace KYC360.WebAPI.Interfaces
{
    public class NamesServices : INameServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public NamesServices(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<bool> AddNames(List<NameRequestDTO> names, string entityId)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var nameDTO in names)
                    {
                        var name = _mapper.Map<Name>(nameDTO);
                        name.EntityId = entityId;
                        await _db.AddAsync(name);
                    }

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Log.Information("Successfully saved names");
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error("Failed to save name", ex);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
