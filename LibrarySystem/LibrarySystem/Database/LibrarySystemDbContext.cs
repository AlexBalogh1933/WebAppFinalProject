using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
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
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Entities.Publisher> Publishers { get; set; }
        public DbSet<Entities.Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Cat in the hat", Genre = "Children's Literature", PublisherId = 1, NumberOfDislikes = 10, NumberOfLikes=5 },
                new Book { BookId = 2, Title = "Green eggs and ham", Genre = "Children's Literature", PublisherId = 1, NumberOfDislikes = 100, NumberOfLikes=10 },
                new Book { BookId = 3, Title = "The lorax", Genre = "Children's Literature", PublisherId = 1, NumberOfDislikes = 50, NumberOfLikes = 15 },
                new Book { BookId = 4, Title = "Fox in socks", Genre = "Children's Literature", PublisherId = 1, NumberOfDislikes = 5, NumberOfLikes = 20 }
            );
            
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Dr.", LastName = "Seuss" }
            );

            modelBuilder.Entity<AuthorBook>().HasData(
                new AuthorBook { BookId = 1, AuthorId= 1 },
                new AuthorBook { BookId = 2, AuthorId = 1 },
                new AuthorBook { BookId = 3, AuthorId = 1 },
                new AuthorBook { BookId = 4, AuthorId = 1 }
            );

            modelBuilder.Entity<Patron>().HasData(
                new Patron { PatronId = 1, FirstName = "David", LastName = "Toran", Address = "123 Red Street", PhoneNumber = "5137784564" },
                new Patron { PatronId = 2, FirstName = "Jack", LastName = "Smith", Address = "456 Orange Street", PhoneNumber = "5135484456" },
                new Patron { PatronId = 3, FirstName = "Purple", LastName = "Poo", Address = "789 Yellow Street", PhoneNumber = "5135564897" }
            );

            modelBuilder.Entity<Entities.Publisher>().HasData(
                new Entities.Publisher { PublisherId = 1, Name = "Houghton Mifflin", Address = "125 High Street Boston", EstablishedYear = 1880 }
            );

            modelBuilder.Entity<Entities.Transaction>().HasData(
                new Entities.Transaction { TransactionId = 1, DateBorrowed = new DateTime(2024, 3, 28), TotalDaysAllowedToBorrow=10, PatronId=1, BookId=1 },
                new Entities.Transaction { TransactionId = 2, DateBorrowed = new DateTime(2024, 3, 28), TotalDaysAllowedToBorrow=100, PatronId=2, BookId=2 },
                new Entities.Transaction { TransactionId = 3, DateBorrowed = new DateTime(2024, 3, 28), TotalDaysAllowedToBorrow = 100, PatronId = 3, BookId = 3 }
            );

            modelBuilder.Entity<Book>()
            .HasMany(e => e.Authors)
            .WithMany(e => e.Books)
            .UsingEntity<AuthorBook>();

            modelBuilder.Entity<Book>()
            .HasOne(e => e.Transaction)
            .WithOne(e => e.Book)
            .HasForeignKey<Transaction>(e => e.BookId)
            .HasPrincipalKey<Book>(e => e.BookId); 
        }
    }
}
