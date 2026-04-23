using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Services;
using bookstore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<CartService>();

builder.Services.AddScoped<OrderState>();

var connectionString = builder.Configuration.GetConnectionString("BOOKIESTORE_DB")
    ?? "Data Source=bookstore.db";

builder.Services.AddDbContextFactory<BookstoreDb>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Bookstore.Data.BookstoreDb>();
    context.Database.Migrate();
    SeedData.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();