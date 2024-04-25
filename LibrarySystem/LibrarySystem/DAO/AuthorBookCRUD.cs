using LibrarySystem.Database;
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

        }

    }
}
