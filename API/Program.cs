using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}); //adding our datacontext as a service and this can be injected to other parts of our application
builder.Services.AddCors(); //this needs to be added because we have our client at different origin(localhost) 


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")) ; //builder is out cors policy builder 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

