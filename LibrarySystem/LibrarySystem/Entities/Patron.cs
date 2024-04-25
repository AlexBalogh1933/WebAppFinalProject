using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    [Table("Patrons")]
    public class Patron
    {
        public int PatronId { get; set; }
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Entities.Transaction>? Patrons { get; set; }
    }
}
