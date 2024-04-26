using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAO
{
    public class AuthorBookCRUD
    {
        public static void initialize(WebApplication app)
        {
            app.MapGet("/api/authorbooks/", async (LibrarySystemDbContext dbContext) =>
            {
                var authorbooks = await dbContext.AuthorBooks.ToListAsync();
                List<AuthorBookViewModel> authorViewModel = new();

                foreach (var author in authorbooks)
                {
                    authorViewModel.Add(new AuthorBookViewModel
                    {
                        AuthorId = author.AuthorId,
                        BookId = author.BookId
                    });
                }
                return Results.Ok(authorViewModel);
            });

            app.MapPost("/api/authorbook/", async (LibrarySystemDbContext dbContext, AuthorBookViewModel authorBookViewModel) =>
            {
                AuthorBook author = new()
                {
                    AuthorId = authorBookViewModel.AuthorId,
                    BookId = authorBookViewModel.BookId,
                };

                await dbContext.AuthorBooks.AddAsync(author);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/api/authors/{author.AuthorId}", author);
            });

            app.MapDelete("/api/authorbook/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var book = dbContext.AuthorBooks.FirstOrDefault(b => b.BookId == id);
                if (book == null)
                {
                    return Results.NotFound();
                }

                dbContext.AuthorBooks.Remove(book);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });

        


        }



    }
}
