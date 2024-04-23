using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    [Table("Transactions")]
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime ReturnDate { get; set; }
        [ForeignKey("PatronId")]
        public int PatronId { get; set; }
        public virtual Patron? Patron { get; set; }
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

    }
}
