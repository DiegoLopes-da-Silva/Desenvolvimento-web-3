using Microsoft.AspNetCore.Identity;
using VasosInteligentes.Data;
using VasosInteligentes.Models;
using VasosInteligentes.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddSingleton<ContextMongoDb>();

// configuração do indentity
var mongoSettings = builder.Configuration
    .GetSection("MongoConnection")
    .Get<MongoSettings>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>
    (options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
    })
    .AddMongoDbStores<ApplicationUser, ApplicationRole, string>
    (mongoSettings.ConnectionString, mongoSettings.Database)
    .AddDefaultTokenProviders();

// importante para scaffolding e as razorpages para o identity
builder.Services.AddRazorPages();

var app = builder.Build();

// seeds
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeeds.SeedRolesAndUser(services, "Admin@123");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

// ten que ser o authentication antes do authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
