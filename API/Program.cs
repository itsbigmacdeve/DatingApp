using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);
// se agrega el serivioc de autenticacion con jwt, se paso a IdentityServiceExtesions
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder=> builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));


app.UseHttpsRedirection();



// add middleware to authenticate the user before mapping controllers,and after the cors
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
// Este codigo se encarga de migrar la base de datos y de crearla si no existe, y ocupa estar desoues de app.MapControllers() y antes de app.Run()
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await context.Database.EnsureCreatedAsync();
    await Seed.SeedUsers(userManager);
    // var userManager = services.GetRequiredService<UserManager<AppUser>>();
    // var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    //await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
