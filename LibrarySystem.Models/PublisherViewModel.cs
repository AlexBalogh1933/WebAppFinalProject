using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class PublisherViewModel
    {
        public int PublisherId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int EstablishedYear { get; set; }
        public virtual ICollection<BookViewModel>? Books { get; set; }
    }
}
