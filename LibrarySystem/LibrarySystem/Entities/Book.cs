using LibrarySystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LibrarySystem.Entities
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int NumberOfLikes { get; set; } = 0;
        public int NumberOfDislikes { get; set; } = 0;

        public int PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; }

        public virtual Transaction? Transaction { get; set; }

        public virtual ICollection<Author>? Authors { get; set; }

    }
}
