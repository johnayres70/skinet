//jpa - 1st thing is we create this application builder which
// has pre configured defaults and is responsible for
//  running our application
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//jpa - using dependency injection you can inject services to
// our controllers, classes etc and the way we do that is utilise
// dependency injection.
// You can inject services into other classes and use their 
// functionality.

// jpa -minimal api start
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//jpa add repo services
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// end

// -- jpa - now we build our app
var app = builder.Build();

// Configure the HTTP request pipeline.
// jpa - as a request comes in think of it as going thru a
// pipeline or tunnel on it's journey into and out of the api
// the request can be manipulated. What we have here is effectively
// midleware. Middleware is the software that you can use to
// do something with that request on it's journey in and out
// of the pipeline

// jpa midleware start
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); 
// jpa removed because will cause warning in our app

app.UseAuthorization();

app.MapControllers(); // jpa - will register our api endpoints so app
                      // will know where to send http request.

// jpa - code to migrate database at app.StartUp
using var scope = app.Services.CreateScope(); // using disposes when finished
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    // create database if it doesnt already exist
    await context.Database.MigrateAsync();
    // seed the database if not already seeded
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{

    logger.LogError(ex, "An error occurred during initial database migration");
}

app.Run();
// jpa middleware end