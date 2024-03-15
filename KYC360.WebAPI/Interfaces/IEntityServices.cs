
using KYC360.WebAPI.Model.DTOs.Entity;
using KYC360.WebAPI.Model.Entities;

namespace KYC360.WebAPI.Interfaces
{
    public interface IEntityServices
    {
        Task<List<Entity>> GetAllEntities(string? search = null);
        Task<Entity?> GetEntityById(string id);
        Task<bool> CreateEntity(EntityRequestDTO entityRequestDTO);
        Task<bool> UpdateEntity(EntityUpdateDTO entityUpdateDTO);
        Task<bool> DeleteEntity(string id);
        Task<List<Entity>> GetAdvancedEntities(string search = null, string gender = null, DateTime? startDate = null, DateTime? endDate = null, List<string> countries = null);
    }
}
