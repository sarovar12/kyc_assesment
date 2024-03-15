using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KYC360.WebAPI.Model.Entities
{
    public class Entity : IEntity
    {
        public bool Deceased { get; set; }
        public string? Gender { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        // Navigation properties
        public List<Address>? Addresses { get; set; }
        public List<Date>? Dates { get; set; }
        public List<Name>? Names { get; set; }
    }

    public interface IEntity
    {
        public bool Deceased { get; set; }
        public string? Gender { get; set; }
        public string Id { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Date>? Dates { get; set; }
        public List<Name>? Names { get; set; }
    }
}
