
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions // making a class static lets us use the methods inside that class without instantiating an object of this class
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){
            
            services.AddDbContext<DataContext>(opt =>
                    {
                        opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
                    }); //adding our datacontext as a service and this can be injected to other parts of our application
            services.AddCors(); //this needs to be added because we have our client at different origin(localhost) 
            services.AddScoped<ITokenService, TokenService>();

            return services;
        } 
        
    }
}