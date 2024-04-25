using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class AuthorBookViewModel
    {
        public int AuthorId { get; set; }
        public AuthorViewModel? Author { get; set; }
        public int BookId { get; set; }
        public BookViewModel? Book { get; set; }
    }
}
