using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    [Table("Transactions")]
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime? ReturnDate { get; set; } = null;
        public DateTime? DateBorrowed { get; set; }
        public int TotalDaysAllowedToBorrow { get; set; }
        public int? PatronId { get; set; } = null;
        public virtual Patron? Patron { get; set; }
        public int? BookId { get; set; } = null;
        public Book? Book { get; set; } = null;
    }
}
