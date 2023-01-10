using App.Doc.IdentityServer.Config;
using App.Doc.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var seed = args.Contains("/seed");

if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}


var builder = WebApplication.CreateBuilder(args);
var defaultConnString = builder.Configuration.GetConnectionString("DefaultDatabaseConnection");
var assemblyName = typeof(Program).Assembly.GetName().Name;

if (seed)
{
SeedData.EnsureSeedData(defaultConnString);
}

builder.Services.AddDbContext<AspNetIdentityDbContext>(options => 
    options.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assemblyName))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(option =>
    {
        option.ConfigureDbContext = b =>
        b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assemblyName));
    }).AddOperationalStore(option =>
    {
        option.ConfigureDbContext = b =>
        b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assemblyName));
    });

//builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
//builder.Services.AddControllers();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endPoint =>
{
    endPoint.MapDefaultControllerRoute();
});
//app.MapControllers();
app.Run();
