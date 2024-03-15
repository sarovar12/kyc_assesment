using AutoMapper;
using KYC360.WebAPI.DatabaseContext;
using KYC360.WebAPI.Model.DTOs.Entity;
using KYC360.WebAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KYC360.WebAPI.Interfaces
{
    public class EntityServices : IEntityServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IDateServices _dateTimeServices;
        private readonly INameServices _nameServices;
        private readonly IAddressServices _addressServices;
        public EntityServices(ApplicationDbContext db, IMapper mapper, IDateServices dateServices, IAddressServices addressServices, INameServices nameServices)
        {
            _db = db;
            _mapper = mapper;
            _dateTimeServices = dateServices;
            _nameServices = nameServices;
            _addressServices = addressServices;
        }
        public async Task<bool> CreateEntity(EntityRequestDTO entityRequestDTO)
        {
            var entity = _mapper.Map<Entity>(entityRequestDTO);
            try
            {
                var result = await _db.Entity.AddAsync(entity);
                await _addressServices.AddAddresses(entityRequestDTO.Addresses, result.Entity.Id);
                await _nameServices.AddNames(entityRequestDTO.Names, result.Entity.Id);
                await _dateTimeServices.AddDates(entityRequestDTO.Dates, result.Entity.Id);
                return true;

            }
            catch
            {
                Log.Error("Failed To Create an Entity");
                return false;
            }

        }

        public async Task<bool> DeleteEntity(string id)
        {
            var entity = await _db.Entity.Where(entity => entity.Id == id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return false;
            }
            try
            {
                _db.Entity.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<List<Entity>> GetAllEntities(string? search = null)
        {
            try
            {
                var entities = await _db.Entity
                    .Include(entity => entity.Addresses)
                    .Include(entity => entity.Names)
                    .Include(entity => entity.Dates)
                    .ToListAsync();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    entities = entities.Where(entity =>
                        entity.Addresses.Any(address =>
                            address.Country.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            address.AddressLine.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        entity.Names.Any(name =>
                            name.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            name.MiddleName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            name.Surname.Contains(search, StringComparison.OrdinalIgnoreCase))
                        ).ToList();
                }

                var entitiesDTO = _mapper.Map<List<Entity>>(entities);
                return entitiesDTO;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get all entities");
                throw;
            }
        }



        public async Task<Entity?> GetEntityById(string id)
        {
            var entity = await _db.Entity.Include(entity => entity.Addresses).Include(entity => entity.Names).Include(entity => entity.Dates).Where(entity => entity.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<Entity>(entity);
        }

        public async Task<bool> UpdateEntity(EntityUpdateDTO entityUpdateDTO)
        {
            var entity = await _db.Entity
                .Include(e => e.Addresses)
                .Include(e => e.Dates)
                .Include(e => e.Names)
                .FirstOrDefaultAsync(e => e.Id == entityUpdateDTO.Id);

            if (entity == null)
                return false;

            try
            {
                _mapper.Map(entityUpdateDTO, entity);

                if (entityUpdateDTO.Addresses != null)
                {
                    foreach (var addressDto in entityUpdateDTO.Addresses)
                    {
                        var existingAddress = entity.Addresses.FirstOrDefault(a => a.Id == addressDto.Id);
                        if (existingAddress != null)
                        {
                            _mapper.Map(addressDto, existingAddress);
                        }
                        else
                        {
                            var newAddress = _mapper.Map<Address>(addressDto);
                            newAddress.EntityId = entity.Id;
                            entity.Addresses.Add(newAddress);
                        }
                    }
                }

                // Update dates
                if (entityUpdateDTO.Dates != null)
                {
                    foreach (var dateDto in entityUpdateDTO.Dates)
                    {
                        var existingDate = entity.Dates.FirstOrDefault(d => d.Id == dateDto.Id);
                        if (existingDate != null)
                        {
                            _mapper.Map(dateDto, existingDate);
                        }
                        else
                        {
                            var newDate = _mapper.Map<Date>(dateDto);
                            newDate.EntityId = entity.Id;
                            entity.Dates.Add(newDate);
                        }
                    }
                }

                //Update names
                if (entityUpdateDTO.Names != null)
                {
                    foreach (var nameDto in entityUpdateDTO.Names)
                    {
                        var existingName = entity.Names.FirstOrDefault(n => n.Id == nameDto.Id);
                        if (existingName != null)
                        {
                            _mapper.Map(nameDto, existingName);
                        }
                        else
                        {
                            var newName = _mapper.Map<Name>(nameDto);
                            newName.EntityId = entity.Id;
                            entity.Names.Add(newName);
                        }
                    }
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to update entity");
                return false;
            }
        }

        public async Task<List<Entity>> GetAdvancedEntities(string search = null, string gender = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<string> countries = null)
        {
            try
            {
                var query = _db.Entity
                    .Include(entity => entity.Addresses)
                    .Include(entity => entity.Names)
                    .Include(entity => entity.Dates)
                    .AsQueryable();

                // Filter by search term if provided
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(entity =>
                        entity.Addresses.Any(address =>
                            address.Country.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            address.AddressLine.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        entity.Names.Any(name =>
                            name.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            name.MiddleName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            name.Surname.Contains(search, StringComparison.OrdinalIgnoreCase))
                    );
                }

                // Filter by gender if provided
                if (!string.IsNullOrWhiteSpace(gender))
                {
                    query = query.Where(entity =>
                        entity.Gender == gender
                    );
                }

                // Filter by start and end dates if provided
                if (startDate != null && endDate != null)
                {
                    query = query.Where(entity =>
                        entity.Dates.Any(date =>
                            date.DateCreated >= startDate && date.DateCreated <= endDate
                        )
                    );
                }

                // Filter by countries if provided
                if (countries != null && countries.Any())
                {
                    query = query.Where(entity =>
                        entity.Addresses.Any(address =>
                            countries.Contains(address.Country)
                        )
                    );
                }

                var entities = await query.ToListAsync();
                var entitiesDTO = _mapper.Map<List<Entity>>(entities);
                return entitiesDTO;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get all entities");
                throw; // Propagate the exception
            }
        }



    }
}