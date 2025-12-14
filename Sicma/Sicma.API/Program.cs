
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Sicma.DataAccess.Context;
using Sicma.DataAccess.SeedData;
using Sicma.Entities;
using Sicma.Repositorys.Implementations;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Implementations;
using Sicma.Service.Interfaces;
using Sicma.Service.Mappers;
using System.Text;

namespace Sicma.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // Add services to the container.            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    "JWT authentication using Bearer schema. \r\n\r\n" +
                    "Add the word 'Bearer' followed by a space and the token in the field below \r\n\r\n" +
                    "Example \"Bearer lñskdjfgañlsdknfa0897asdfjkansdfl\" ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {

                            Reference = new OpenApiReference
                            {
                                Type= ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme= "oauth2",
                            Name= "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1.0",
                    Title = "Sicma backend V1",
                    Description = "Sicma API",
                    Contact = new OpenApiContact
                    {
                        Name = "test name",
                        Url = new Uri("http://localhost")
                    }
                });
            });

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<DbSicmaContext>(options =>
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
            builder.Services.AddScoped<IOperationConfigRepository, OperationConfigRepository>();
            builder.Services.AddScoped<ITokenHistoryRepository, TokenHistoryRepository>();
            builder.Services.AddScoped<IPracticeConfigRepository, PracticeConfigRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IInstitutionService, InstitutionService>();
            builder.Services.AddScoped<IOperationConfigService, OperationConfigService>();
            builder.Services.AddScoped<ITokenHistoryService, TokenHistoryService>();
            builder.Services.AddScoped<IPracticeConfigService, PracticeConfigService>();

            builder.Services.AddAutoMapper(p =>
            {
                p.AddProfile<UserMap>();
                p.AddProfile<InstitutionMap>();
                p.AddProfile<OperationConfigMap>();
                p.AddProfile<TokenHistoryMap>();
                p.AddProfile<PracticeConfigMap>();
            });

            //Add access to configuration
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            var key = builder.Configuration.GetValue<string>("APISettings:secretkey");

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    
                }
                ).AddJwtBearer(x =>
                {
                    x.MapInboundClaims = false;
                    x.RequireHttpsMetadata = false; //in production need to true
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();

                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //support for authentication
            app.UseAuthentication();

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
