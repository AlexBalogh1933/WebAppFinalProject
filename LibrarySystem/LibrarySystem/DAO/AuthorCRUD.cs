using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAO
{
    public static class AuthorCRUD
    {
        public static void initialize(WebApplication app)
        {
            app.MapGet("/api/authors/", async (LibrarySystemDbContext dbContext) =>
            {
                var authors = await dbContext.Authors.ToListAsync();
                List<AuthorViewModel> authorViewModel = new();

                foreach (var author in authors)
                {
                    authorViewModel.Add(new AuthorViewModel
                    {
                        AuthorId = author.AuthorId,
                        FirstName = author.FirstName,
                        LastName = author.LastName
                    });
                }
                return Results.Ok(authorViewModel);
            });

            app.MapGet("/api/author/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var author = await dbContext.Authors.FindAsync(id);

                if (author == null)
                {
                    return Results.NotFound();
                }

                AuthorViewModel authorViewModel = new()
                {
                    AuthorId = author.AuthorId,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };

                return Results.Ok(author);
            });

            app.MapPost("/api/author/", async (LibrarySystemDbContext dbContext, AuthorViewModel authorViewModel) =>
            {
                Author author = new()
                {
                    FirstName = authorViewModel.FirstName,
                    LastName = authorViewModel.LastName,
                };

                await dbContext.Authors.AddAsync(author);

                await dbContext.SaveChangesAsync();
                return Results.Created($"/api/authors/{author.AuthorId}", author);
            });

            app.MapPut("/api/author/{id}", async (LibrarySystemDbContext dbContext, int id, AuthorViewModel authorViewModel) =>
            {
                var author = await dbContext.Authors.FindAsync(id);

                if (author == null)
                {
                    return Results.NotFound();
                }

                author.FirstName = authorViewModel.FirstName;
                author.LastName = authorViewModel.LastName;

                await dbContext.SaveChangesAsync();
                return Results.Ok(author);
            });

            app.MapDelete("/api/author/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var author = await dbContext.Authors.FindAsync(id);

                if (author == null)
                {
                    return Results.NotFound();
                }

                dbContext.Authors.Remove(author);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });

        }
    }
}
