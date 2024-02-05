using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")) ; //builder is out cors policy builder 



app.UseHttpsRedirection();

app.UseAuthentication(); //middleware which has been intentionally placed after UseCors and before MapControllers . This asks that 'Do you have a valid token?'
                        // Example: i go to a night club at age 16 whereas to enter i should be 18, bouncer says that even though this is a valid id
                        // you still cant enter the nightclub as you should be above 18. So i am authenticated but not authorized.

app.UseAuthorization(); // This asks "Ok you have a valid token, What are you allowed to do?"

app.MapControllers();

app.Run();

