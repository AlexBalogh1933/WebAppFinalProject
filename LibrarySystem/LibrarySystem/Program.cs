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

app.MapGet("/api/books/", async (LibrarySystemDbContext dbContext) =>
{
    var books = await dbContext.Books.ToListAsync();
    List<BookViewModel> bookViewModels = new();

    foreach (var book in books)
    {
        bookViewModels.Add(new BookViewModel 
        {
            BookId = book.BookId,
            Title = book.Title,
            AuthorId = book.AuthorId,
            Genre = book.Genre,
            PublisherId = book.PublisherId,
        });
    }
    return Results.Ok(bookViewModels);
});

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

app.MapGet("/api/book/{id}", async (LibrarySystemDbContext dbContext, int id) =>
{
    var book = await dbContext.Books.FindAsync(id);

    if(book == null)
    {
        return Results.NotFound();
    }

    BookViewModel bookViewModels = new()
    {
        BookId = book.BookId,
        Title = book.Title,
        AuthorId = book.AuthorId,
        Genre = book.Genre,
        PublisherId = book.PublisherId,
    };

    return Results.Ok(book);
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

app.MapPost("/api/book/", async (LibrarySystemDbContext dbContext, BookViewModel bookViewModel) =>
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

app.Run();
