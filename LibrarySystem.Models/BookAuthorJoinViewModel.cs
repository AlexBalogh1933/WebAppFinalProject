using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookAuthorJoinViewModel
    {
        public BookViewModel Book { get; set; }
        public AuthorViewModel Author { get; set; }
    }

}
