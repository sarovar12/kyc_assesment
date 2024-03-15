using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KYC360.WebAPI.Model.Entities
{
    public class Date
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("Entity")]
        public string EntityId { get; set; }
        public string? DateType { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
