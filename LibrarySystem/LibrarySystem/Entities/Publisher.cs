using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Entities
{
    [Table("Publishers")]
    public class Publisher
    {
        public int PublisherId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Address { get; set; }
        [MaxLength(50)]
        public int EstablishedYear { get; set; }
        
    }
}
