using Application.Abstractions;
using Application.Movimiento.Commands;
using Application.Movimiento.Queries;
using challenge_metafar.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace challenge_metafar.Extensions
{
    public static class Extensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IATMRepository, TarjetaRepository>();
            builder.Services.AddMediatR(typeof(ExtraerSaldo));
            builder.Services.AddMediatR(typeof(ObtenerSaldoPorNroTarjeta));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNameCaseInsensitive = false;
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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
