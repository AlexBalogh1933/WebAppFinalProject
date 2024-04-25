using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LibrarySystem.Models
{
    public class PatronViewModel
    {
        public int PatronId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public int? TransactionId { get; set; } = null;
        public virtual TransactionViewModel? Transaction { get; set; }
    }
}
