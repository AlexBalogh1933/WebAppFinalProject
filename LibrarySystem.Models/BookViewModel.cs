using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int NumberOfLikes { get; set; } = 0;
        public int NumberOfDislikes { get; set; } = 0;


        public int PublisherId { get; set; }
        public virtual PublisherViewModel? Publisher { get; set; }

        public virtual ICollection<AuthorViewModel>? Authors { get; set; }

    }
}
