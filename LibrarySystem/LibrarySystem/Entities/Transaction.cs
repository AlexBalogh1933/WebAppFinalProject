using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    [Table("Transactions")]
    public class Transaction
    {
        public int TransactionId { get; set; }
        [MaxLength(50)]
        public int PatronId { get; set; }
        [MaxLength(50)]
        public int BookID { get; set; }
        [MaxLength(50)]
        public DateTime ReturnDate { get; set; }

    }
}
