var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MangaBook_Models.MangaDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        //Hier kunne  we initialisatiecode plaatsen, zoals seeden van de database
        MangaBook_Models.MangaDbContext context = services.GetRequiredService<MangaBook_Models.MangaDbContext>();
        MangaBook_Models.MangaDbContext.Seeder(context);
    }
    catch (Exception ex)
    {
        //log de error
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }

    // Configure the HTTP request pipeline.
    //hoe ik de url onderzoek en aanpas voor de static assets
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();
}

app.Run();
