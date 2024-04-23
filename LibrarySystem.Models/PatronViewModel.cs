using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class PatronViewModel
    {
        public int PatronId { get; set; }
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}
