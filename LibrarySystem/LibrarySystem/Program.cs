using LibrarySystem.Client.Pages;
using LibrarySystem.Components;
using LibrarySystem.Database;
using LibrarySystem.Entities;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using System;

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
            Author = book.Author,
            Genre = book.Genre,
            Publisher = book.Publisher,
        });
    }
    return Results.Ok(bookViewModels);
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
        Author = book.Author,
        Genre = book.Genre,
        Publisher = book.Publisher
    };

    return Results.Ok(book);
});

app.MapPost("/api/book/", async (LibrarySystemDbContext dbContext, BookViewModel bookViewModel) =>
{
    Book book = new()
    {
        Title = bookViewModel.Title,
        Author = bookViewModel.Author,
        Genre = bookViewModel.Genre,
        Publisher = bookViewModel.Publisher
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
    book.Author = bookViewModel.Author;
    book.Genre = bookViewModel.Genre;
    book.Publisher = bookViewModel.Publisher;

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

app.Run();
