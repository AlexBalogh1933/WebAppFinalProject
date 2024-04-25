using LibrarySystem.Client.Pages;
using LibrarySystem.Components;
using LibrarySystem.DAO;
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


AuthorCRUD.initialize(app);
BookCRUD.initialize(app);
PatronCRUD.initialize(app);
PublisherCRUD.initialize(app);
TransactionCRUD.initialize(app);
AuthorBookCRUD.initialize(app);

app.Run();
