using Application.Abstractions;
using Application.Movimiento.Commands;
using Application.Movimiento.Queries;
using challenge_metafar.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Application.Movimientos.Queries;
using Microsoft.Extensions.Options;

namespace challenge_metafar.Extensions
{
    public static class Extensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IATMRepository, TarjetaRepository>();
            builder.Services.AddMediatR(typeof(Login));
            builder.Services.AddMediatR(typeof(ObtenerSaldoPorNroTarjeta));
            builder.Services.AddMediatR(typeof(ExtraerSaldo));
            builder.Services.AddMediatR(typeof(ObtenerOperaciones));


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{typeof(Program).Assembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });


            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNameCaseInsensitive = false;
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            
            builder.Services.AddSwaggerGen(c => {
                            c.SwaggerDoc("v1", new OpenApiInfo
                            {
                                Title = "Metafar Challenge",
                                Version = "v1",
                                Description = "API Challenge para la empresa Metafar, la api simula un cajero ATM la cual realiza el login con Token JWT, consulta de saldo, retiro de dinero y consulta de historial de operaciónes",
                                Contact = new OpenApiContact
                                {
                                    Name = "Santiago Gonzalez",
                                    Email = "santigonzalez05@gmail.com",
                                },
                            });
                            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                            {
                                Name = "Authorization",
                                Type = SecuritySchemeType.ApiKey,
                                Scheme = "Bearer",
                                BearerFormat = "JWT",
                                In = ParameterLocation.Header,
                                Description = "JWT Authorization header usando esquema Bearer. \r\n\r\n Ingresar 'Bearer' [space] y luego el token obtenido en el endpoint auth",
                            });
                            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void RegisterEdpointDefinitions(this WebApplication app,WebApplicationBuilder builder)
        {
            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
                    && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.RegisterEndpoints(app,builder);
            }
        }
    }
}
