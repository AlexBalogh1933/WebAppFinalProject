using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Database
{
    public class LibrarySystemDbContext : DbContext
    {
        public LibrarySystemDbContext(DbContextOptions<LibrarySystemDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Cat in the hat", Author = "Dr. Suess", Genre = "Comedy", Publisher = "Houghton Mifflin" }
            );

            modelBuilder.Entity<Patron>().HasData(
                new Patron { PatronId = 1, FirstName = "David", LastName = "Toran", Address = "1225 Green Street", PhoneNumber = "5137784564" }
            );
        }
    }
}
