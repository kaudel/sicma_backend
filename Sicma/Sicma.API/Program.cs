
using Microsoft.EntityFrameworkCore;
using Sicma.DataAccess.Context;
using Sicma.Repositorys.Implementations;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Implementations;
using Sicma.Service.Interfaces;

namespace Sicma.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddDbContext<DbsicmaContext>( options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBSicma"));
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
