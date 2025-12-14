using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AspNetCore.Unobtrusive.Ajax;
using Manga_Web.Services;


var builder = WebApplication.CreateBuilder(args);

//Get the database connection string.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=MangaBookDB;Trusted_Connection=True;MultipleActiveResultSets=true";

//Add the DbContext for Entity Framework.
builder.Services.AddDbContext<MangaBook_Models.MangaDbContext>();

builder.Services.AddDefaultIdentity<MangaUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MangaDbContext>();

//toevoegen van userManager /SignInmanager voor Mangauser
builder.Services.AddScoped(typeof(SignInManager<Microsoft.AspNetCore.Identity.IdentityUser>), 
sp => sp.GetRequiredService<SignInManager<MangaUser>>());



builder.Logging.AddDbLogger(options => {
    builder.Configuration.GetSection("Logging");
});

//Add support for MVC controllers and Razor Pages (for the Identity UI).
builder.Services.AddControllersWithViews();

//Voor de configuratie van de resfull API's
builder.Services.AddControllers();

//voor het gebruik van swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MangaAPI", Version = "v1" });
});

//localisatie toevoegen voor meertaligheid
builder.Services.AddLocalization(options => options.ResourcesPath = "Translations");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

//registratie voor de Unobtrusive Ajax library
builder.Services.AddUnobtrusiveAjax();



var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        MangaBook_Models.MangaDbContext context = services.GetRequiredService<MangaBook_Models.MangaDbContext>();
        MangaBook_Models.MangaDbContext.Seeder(context);
    }
    catch(Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MangaAPI v1"));
}

//toevoegen van middleware voor localisatie
var supportedCultures = new[] { "nl", "en", "fr"};
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

//Configure the HTTP request pipeline
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseUnobtrusiveAjax();

app.UseRouting();

// 5. Add Authentication and Authorization middleware.
// This must be placed after UseRouting.
app.UseAuthentication();
app.UseAuthorization();

// 6. Map the endpoints for MVC controllers and Razor Pages.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); //This is crucial for the login pages to work.

app.MapControllers();

// Assign the application instance to the static property.
Manga_Web.Services.Globals.App = app; 

app.Run();
