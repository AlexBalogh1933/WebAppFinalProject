using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Entities
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Author { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; }
        [MaxLength(50)]
        public string Publisher { get; set; }
    }
}
