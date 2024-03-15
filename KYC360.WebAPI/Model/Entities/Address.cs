using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KYC360.WebAPI.Model.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        [ForeignKey("Entity")]
        public string EntityId { get; set; }

    }
}
