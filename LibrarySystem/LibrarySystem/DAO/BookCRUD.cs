using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAO
{
    public static class BookCRUD
    {
        public static void initialize(WebApplication app)
        {
            app.MapGet("/api/books/", async (LibrarySystemDbContext dbContext) =>
            {
                var books = await dbContext.Books.ToListAsync();
                List<BookViewModel> bookViewModel = new();

                foreach (var book in books)
                {
                    bookViewModel.Add(new BookViewModel
                    {
                        BookId = book.BookId,
                        Title = book.Title,
                        Genre = book.Genre,
                        PublisherId = book.PublisherId,
                        NumberOfDislikes = book.NumberOfDislikes,
                        NumberOfLikes = book.NumberOfLikes
                    });
                }
                return Results.Ok(bookViewModel);
            });

            app.MapGet("/api/book/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var book = await dbContext.Books.FindAsync(id);

                if (book == null)
                {
                    return Results.NotFound();
                }

                BookViewModel bookViewModel = new()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Genre = book.Genre,
                    PublisherId = book.PublisherId,
                    NumberOfDislikes = book.NumberOfDislikes,
                    NumberOfLikes = book.NumberOfLikes
                };

                return Results.Ok(book);
            });

            app.MapPost("/api/book", async (LibrarySystemDbContext dbContext, BookViewModel bookViewModel) =>
            {
                Book book = new()
                {
                    Title = bookViewModel.Title,
                    Genre = bookViewModel.Genre,
                    PublisherId = bookViewModel.PublisherId,
                };

                await dbContext.Books.AddAsync(book);

                await dbContext.SaveChangesAsync();
                return Results.Created($"/api/books/{book.BookId}", book);
            });

            app.MapPut("/api/book/{id}", async (LibrarySystemDbContext dbContext, int id, BookViewModel bookViewModel) =>
            {
                var book = await dbContext.Books.FindAsync(id);

                if (book == null)
                {
                    return Results.NotFound();
                }

                book.Title = bookViewModel.Title;
                book.Genre = bookViewModel.Genre;
                book.PublisherId = bookViewModel.PublisherId;

                await dbContext.SaveChangesAsync();
                return Results.Ok(book);
            });

            app.MapDelete("/api/book/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var book = await dbContext.Books.FindAsync(id);

                if (book == null)
                {
                    return Results.NotFound();
                }

                dbContext.Books.Remove(book);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}