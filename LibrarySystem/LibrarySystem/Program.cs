using LibrarySystem.Client.Pages;
using LibrarySystem.Components;
using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Net;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();


builder.Services.AddDbContext<LibrarySystemDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LibDB");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LibrarySystem.Client._Imports).Assembly);

// BOOKS
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
            AuthorId = book.AuthorId,
            Genre = book.Genre,
            PublisherId = book.PublisherId,
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
        AuthorId = book.AuthorId,
        Genre = book.Genre,
        PublisherId = book.PublisherId,
    };

    return Results.Ok(book);
});

app.MapPost("/api/book", async (LibrarySystemDbContext dbContext, BookViewModel bookViewModel) =>
{
    Book book = new()
    {
        Title = bookViewModel.Title,
        Genre = bookViewModel.Genre,
        AuthorId = bookViewModel.AuthorId,
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
    book.AuthorId = bookViewModel.AuthorId;
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

// AUTHORS
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
            LastName = author.LastName,
            Publications = author.Publications
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
        LastName = author.LastName,
        Publications = author.Publications
    };

    return Results.Ok(author);
});

app.MapPost("/api/author/", async (LibrarySystemDbContext dbContext, AuthorViewModel authorViewModel) =>
{
    Author author = new()
    {
        FirstName = authorViewModel.FirstName,
        LastName = authorViewModel.LastName,
        Publications = authorViewModel.Publications
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
    author.Publications = authorViewModel.Publications;

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

// PATRONS
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

// PUBLISHERS
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

// TRANSACTIONS
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
            PatronId = transaction.PatronId,
            BookId = transaction.BookId
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
        PatronId = transaction.PatronId,
        BookId = transaction.BookId
    };

    return Results.Ok(transaction);
});

app.MapPost("/api/transaction/", async (LibrarySystemDbContext dbContext, TransactionViewModel transactionViewModel) =>
{
    Transaction transaction = new()
    {
        ReturnDate = transactionViewModel.ReturnDate,
        PatronId = transactionViewModel.PatronId,
        BookId = transactionViewModel.BookId
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
    transaction.PatronId = transactionViewModel.PatronId;
    transaction.BookId = transactionViewModel.BookId;

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

app.Run();
