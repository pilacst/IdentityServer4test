using App.Doc.API.Services;
using App.Doc.DbAccess;
using App.Doc.Repository.Repositories.ChannelCenter;
using App.Doc.Services.Contracts;
using App.Doc.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.Authority = "https://localhost:5443";
        options.ApiName = "App.Doc.API";
    });

builder.Services.Configure<IdentityServerSettings>(builder.Configuration.GetSection("IdentityServerSettings"));

//Register applicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabaseConnection")));

builder.Services.AddScoped<IChannelCenterRepository, ChannelCenterRepository>();
builder.Services.AddScoped<IChannelCenterService, ChannelCenterService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endPoint =>
{
    endPoint.MapDefaultControllerRoute();
});

app.Run();
