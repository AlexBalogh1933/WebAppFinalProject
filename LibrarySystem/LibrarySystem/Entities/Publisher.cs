using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Entities
{
    [Table("Publishers")]
    public class Publisher
    {
        public int PublisherId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        public int EstablishedYear { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
