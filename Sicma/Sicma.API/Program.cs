
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Sicma.DataAccess.Context;
using Sicma.DataAccess.SeedData;
using Sicma.Entities;
using Sicma.Repositorys.Implementations;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Implementations;
using Sicma.Service.Interfaces;
using Sicma.Service.Mappers;

namespace Sicma.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // Add services to the container.
            //builder.Services.AddOpenApi();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1.0",
                    Title= "Sicma backend V1",
                    Description ="Sicma API",
                    Contact = new OpenApiContact {
                        Name= "test name", 
                        Url = new Uri("http://localhost")
                    }
                });
            });
        
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<DbSicmaContext>( options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBSicma"));
            });

            //configure Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(policy =>
            {
                policy.Password.RequireDigit = true;
                policy.Password.RequireLowercase = true;
                policy.Password.RequiredLength = 8;
                policy.Password.RequireNonAlphanumeric = false;
                policy.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DbSicmaContext>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IInstitutionService, InstitutionService>();

            builder.Services.AddAutoMapper(p =>
            {
                p.AddProfile<UserMap>();
            });
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();

                app.UseSwagger();
                //app.UseSwaggerUI( options => 
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sicma API");
                //    options.RoutePrefix = "";
                //});
                app.UseSwaggerUI();
            }            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await UserRoleDataSeed.Initialize(services);
            }

            app.Run();
        }
    }
}
