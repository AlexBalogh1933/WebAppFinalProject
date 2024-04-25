using LibrarySystem.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Entities
{
    [Table("Authors")]
    public class Author
    {
        public int AuthorId { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
