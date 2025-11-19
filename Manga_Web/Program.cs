using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Add this using directive


var builder = WebApplication.CreateBuilder(args);

// 1. Get the database connection string.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=MangaBookDB;Trusted_Connection=True;MultipleActiveResultSets=true";

// 2. Add the DbContext for Entity Framework.
builder.Services.AddDbContext<MangaBook_Models.MangaDbContext>();

builder.Services.AddDefaultIdentity<MangaUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MangaDbContext>();



// 4. Add support for MVC controllers and Razor Pages (for the Identity UI).
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 5. Add Authentication and Authorization middleware.
// This must be placed after UseRouting.
app.UseAuthentication();
app.UseAuthorization();

// 6. Map the endpoints for MVC controllers and Razor Pages.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // This is crucial for the login pages to work.

app.Run();
