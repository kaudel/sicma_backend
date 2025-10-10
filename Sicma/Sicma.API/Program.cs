
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sicma.DataAccess.Context;
using Sicma.Repositorys.Implementations;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Implementations;
using Sicma.Service.Interfaces;
using Sicma.Service.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;

namespace Sicma.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // Add services to the container.
            builder.Services.AddOpenApi();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
        
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<DbsicmaContext>( options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBSicma"));
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAutoMapper(p =>
            {
                p.AddProfile<UserMap>();
            });
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
