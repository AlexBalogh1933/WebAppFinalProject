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
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        [ForeignKey("PublisherId")]
        public int PublisherId { get; set; }

    }
}
