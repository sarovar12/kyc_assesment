using AutoMapper;
using KYC360.WebAPI.DatabaseContext;
using KYC360.WebAPI.Model.DTOs.Date;
using KYC360.WebAPI.Model.Entities;
using Serilog;

namespace KYC360.WebAPI.Interfaces
{
    public class DateServices : IDateServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public DateServices(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> AddDates(List<DateRequestDTO> dates, string entityId)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var dateDTO in dates)
                    {
                        var date = _mapper.Map<Date>(dateDTO);
                        date.EntityId = entityId;
                        await _db.AddAsync(date);
                    }

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Log.Information("Successfully saved dates");
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Date save failed, making another attempt");
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
