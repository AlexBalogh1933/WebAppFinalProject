using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace LibrarySystem.Database
{
    public class LibrarySystemDbContext : DbContext
    {
        public LibrarySystemDbContext(DbContextOptions<LibrarySystemDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Entities.Publisher> Publishers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Cat in the hat", AuthorId = 1, Genre = "Comedy", PublisherId = 1 }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Dr.", LastName = "Suess", Publications = 3 }
            );

            modelBuilder.Entity<Patron>().HasData(
                new Patron { PatronId = 1, FirstName = "David", LastName = "Toran", Address = "1225 Green Street", PhoneNumber = "5137784564" }
            );

            modelBuilder.Entity<Entities.Publisher>().HasData(
                new Entities.Publisher { PublisherId = 1, Name = "Houghton Mifflin", Address = "125 High Street Boston", EstablishedYear = 1880 }
            );
        }
        public DbSet<LibrarySystem.Entities.Transaction> Transaction { get; set; } = default!;
        public DbSet<LibrarySystem.Entities.Author> Author { get; set; } = default!;
        //public DbSet<LibrarySystem.Entities.Publisher> Publisher { get; set; } = default!;
    }
}
