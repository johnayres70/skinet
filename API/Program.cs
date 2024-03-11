//jpa - 1st thing is we create this application builder which
// has pre configured defaults and is responsible for
//  running our application
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
}

);
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

app.Run();
// jpa middleware end