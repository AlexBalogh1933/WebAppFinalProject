using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAO
{
    public static class TransactionCRUD
    {
        public static void initialize(WebApplication app)
        {
            app.MapGet("/api/transactions/", async (LibrarySystemDbContext dbContext) =>
            {
                var transactions = await dbContext.Transactions.ToListAsync();
                List<TransactionViewModel> transactionViewModel = new();

                foreach (var transaction in transactions)
                {
                    transactionViewModel.Add(new TransactionViewModel
                    {
                        TransactionId = transaction.TransactionId,
                        ReturnDate = transaction.ReturnDate,
                        TotalDaysAllowedToBorrow = transaction.TotalDaysAllowedToBorrow,
                        DateBorrowed = transaction.DateBorrowed,

                    });
                }
                return Results.Ok(transactionViewModel);
            });

            app.MapGet("/api/transaction/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var transaction = await dbContext.Transactions.FindAsync(id);

                if (transaction == null)
                {
                    return Results.NotFound();
                }

                TransactionViewModel transactionViewModel = new()
                {
                    TransactionId = transaction.TransactionId,
                    ReturnDate = transaction.ReturnDate,
                    TotalDaysAllowedToBorrow = transaction.TotalDaysAllowedToBorrow,
                    DateBorrowed = transaction.DateBorrowed,

                };

                return Results.Ok(transaction);
            });

            app.MapPost("/api/transaction/", async (LibrarySystemDbContext dbContext, TransactionViewModel transactionViewModel) =>
            {
                Entities.Transaction transaction = new()
                {
                    ReturnDate = transactionViewModel.ReturnDate,
                    TotalDaysAllowedToBorrow = transactionViewModel.TotalDaysAllowedToBorrow,
                    DateBorrowed = transactionViewModel.DateBorrowed,
                };

                await dbContext.Transactions.AddAsync(transaction);

                await dbContext.SaveChangesAsync();
                return Results.Created($"/api/transactions/{transaction.TransactionId}", transaction);
            });

            app.MapPut("/api/transaction/{id}", async (LibrarySystemDbContext dbContext, int id, TransactionViewModel transactionViewModel) =>
            {
                var transaction = await dbContext.Transactions.FindAsync(id);

                if (transaction == null)
                {
                    return Results.NotFound();
                }

                transaction.ReturnDate = transactionViewModel.ReturnDate;
                transaction.DateBorrowed = transactionViewModel.DateBorrowed;
                transaction.TotalDaysAllowedToBorrow = transactionViewModel.TotalDaysAllowedToBorrow;

                await dbContext.SaveChangesAsync();
                return Results.Ok(transaction);
            });

            app.MapDelete("/api/transaction/{id}", async (LibrarySystemDbContext dbContext, int id) =>
            {
                var transaction = await dbContext.Transactions.FindAsync(id);

                if (transaction == null)
                {
                    return Results.NotFound();
                }

                dbContext.Transactions.Remove(transaction);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
