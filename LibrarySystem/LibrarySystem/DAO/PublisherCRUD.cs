using LibrarySystem.Database;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAO
{
    public static class PublisherCRUD
    {
        public static void initialize(WebApplication app)
        {
            app.MapGet("/api/publishers/", async (LibrarySystemDbContext dbContext) =>
            {
                var publishers = await dbContext.Publishers.ToListAsync();
                List<PublisherViewModel> publisherViewModel = new();

                foreach (var publisher in publishers)
                {
                    publisherViewModel.Add(new PublisherViewModel
                    {
                        PublisherId = publisher.PublisherId,
                        Name = publisher.Name,
                        Address = publisher.Address,
                        EstablishedYear = publisher.EstablishedYear
                    });
                }
                return Results.Ok(publisherViewModel);
            });

            app.MapGet("/api/publisher/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var publisher = await dbContext.Publishers.FindAsync(id);

                if (publisher == null)
                {
                    return Results.NotFound();
                }

                PublisherViewModel publisherViewModel = new()
                {
                    PublisherId = publisher.PublisherId,
                    Name = publisher.Name,
                    Address = publisher.Address,
                    EstablishedYear = publisher.EstablishedYear
                };

                return Results.Ok(publisher);
            });

            app.MapPost("/api/publisher/", async (LibrarySystemDbContext dbContext, PublisherViewModel publisherViewModel) =>
            {
                LibrarySystem.Entities.Publisher publisher = new()
                {
                    Name = publisherViewModel.Name,
                    Address = publisherViewModel.Address,
                    EstablishedYear = publisherViewModel.EstablishedYear
                };

                await dbContext.Publishers.AddAsync(publisher);

                await dbContext.SaveChangesAsync();
                return Results.Created($"/api/publishers/{publisher.PublisherId}", publisher);
            });

            app.MapPut("/api/publisher/{id}", async (LibrarySystemDbContext dbContext, int id, PublisherViewModel publisherViewModel) =>
            {
                var publisher = await dbContext.Publishers.FindAsync(id);

                if (publisher == null)
                {
                    return Results.NotFound();
                }


                publisher.Name = publisherViewModel.Name;
                publisher.Address = publisherViewModel.Address;
                publisher.EstablishedYear = publisherViewModel.EstablishedYear;

                await dbContext.SaveChangesAsync();
                return Results.Ok(publisher);
            });

            app.MapDelete("/api/publisher/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var publisher = await dbContext.Publishers.FindAsync(id);

                if (publisher == null)
                {
                    return Results.NotFound();
                }

                dbContext.Publishers.Remove(publisher);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });

        }
    }
}
