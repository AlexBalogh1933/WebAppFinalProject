using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAO
{
    public static class PatronCRUD
    {
        public static void initialize(WebApplication app)
        {
            app.MapGet("/api/Patrons/", async (LibrarySystemDbContext dbContext) =>
            {
                var patrons = await dbContext.Patrons.ToListAsync();
                List<PatronViewModel> patronViewModel = new();

                foreach (var patron in patrons)
                {
                    patronViewModel.Add(new PatronViewModel
                    {
                        PatronId = patron.PatronId,
                        FirstName = patron.FirstName,
                        LastName = patron.LastName,
                        Address = patron.Address,
                        PhoneNumber = patron.PhoneNumber
                    });
                }
                return Results.Ok(patronViewModel);
            });

            app.MapGet("/api/patron/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var patron = await dbContext.Patrons.FindAsync(id);

                if (patron == null)
                {
                    return Results.NotFound();
                }

                PatronViewModel patronViewModel = new()
                {
                    PatronId = patron.PatronId,
                    FirstName = patron.FirstName,
                    LastName = patron.LastName,
                    Address = patron.Address,
                    PhoneNumber = patron.PhoneNumber
                };

                return Results.Ok(patron);
            });

            app.MapPost("/api/patron/", async (LibrarySystemDbContext dbContext, PatronViewModel patronViewModel) =>
            {
                Patron patron = new()
                {
                    FirstName = patronViewModel.FirstName,
                    LastName = patronViewModel.LastName,
                    Address = patronViewModel.Address,
                    PhoneNumber = patronViewModel.PhoneNumber
                };

                await dbContext.Patrons.AddAsync(patron);

                await dbContext.SaveChangesAsync();
                return Results.Created($"/api/patrons/{patron.PatronId}", patron);
            });

            app.MapPut("/api/patron/{id}", async (LibrarySystemDbContext dbContext, int id, PatronViewModel patronViewModel) =>
            {
                var patron = await dbContext.Patrons.FindAsync(id);

                if (patron == null)
                {
                    return Results.NotFound();
                }

                patron.FirstName = patronViewModel.FirstName;
                patron.LastName = patronViewModel.LastName;
                patron.Address = patronViewModel.Address;
                patron.PhoneNumber = patronViewModel.PhoneNumber;

                await dbContext.SaveChangesAsync();
                return Results.Ok(patron);
            });

            app.MapDelete("/api/patron/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var patron = await dbContext.Patrons.FindAsync(id);

                if (patron == null)
                {
                    return Results.NotFound();
                }

                dbContext.Patrons.Remove(patron);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });

        }
    }
}
