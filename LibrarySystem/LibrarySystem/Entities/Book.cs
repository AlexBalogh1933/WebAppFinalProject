using LibrarySystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Entities
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; } = null;
        [ForeignKey("PublisherId")]
        public int PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; } = null;
    }
}
