using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? DateBorrowed { get; set; }
        public int TotalDaysAllowedToBorrow { get; set; }
        public virtual ICollection<PatronViewModel>? Patrons { get; set; }
    }
}
