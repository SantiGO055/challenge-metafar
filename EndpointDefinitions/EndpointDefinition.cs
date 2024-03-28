using Application.Movimiento.Queries;
using challenge_metafar.Abstractions;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Application.Abstractions;
using Domain.Models;
using Application.Movimiento.Commands;
using Application.Movimientos.Queries;

namespace challenge_metafar.EndpointDefinitions
{
    public class EndpointDefinition : IEndpointDefinition
    {

        
        public void RegisterEndpoints(WebApplication app, WebApplicationBuilder builder)
        {
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "challenge-metafar-v1");
                });

            }

            app.MapGet("/api/auth/{tarjeta}/{pin}", async (IMediator mediator, int tarjeta, int pin) =>
            {
                var login = new Login { NroTarjeta = tarjeta, Pin = pin};
                var resultLogin = await mediator.Send(login);

                if (!resultLogin.IsError)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);

                    var tokenDes = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, tarjeta.ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDes);
                    ServiceResult<LoginResponse> result = new();
                    var loginRes = new LoginResponse();
                    loginRes.Token = tokenHandler.WriteToken(token);
                    result.Payload = loginRes;
                    return Results.Ok(result);
                }
                else
                {
                    return Results.Problem(resultLogin.Message, null,StatusCodes.Status404NotFound);
                }
               
                
            })
            .Produces<ServiceResult<Tarjeta>>(StatusCodes.Status200OK)
            .WithSummary("Genera token JWT")
            .WithDescription("Genera Token JWT para luego utilizarlo en los demas endpoints protegidos")
            .WithOpenApi();

            app.MapGet("/api/getSaldo/{tarjeta}", async (IMediator mediator, int tarjeta) =>
            {
                var obtenerTarjeta = new ObtenerSaldoPorNroTarjeta { tarjeta = tarjeta };
                var result = await mediator.Send(obtenerTarjeta);
                Console.WriteLine(result);
                return Results.Ok(result);
            }).RequireAuthorization()
            .Produces<ServiceResult<Saldo>>(StatusCodes.Status200OK)
            .WithSummary("Obtiene resumen de la tarjeta")
            .WithDescription("Obtiene un resumen dado un nro de tarjeta retorna nombre de usuario, numero de cuenta, saldo actual y fecha de la ultima extraccion")
            .WithOpenApi();

            app.MapPost("/api/retirar/", async (IMediator mediator,int tarjeta, decimal monto) =>
            {
                var extraerSaldo = new ExtraerSaldo { tarjeta = tarjeta, saldo = monto };
                var result = await mediator.Send(extraerSaldo);
                if (result.IsError)
                {
                    return Results.Problem(result.Message,null,StatusCodes.Status404NotFound);
                }
                else
                {
                    return Results.Ok(result);
                }
            }).RequireAuthorization()
            .Produces<ServiceResult<Saldo>>(StatusCodes.Status200OK)
            .Produces<ServiceResult<Saldo>>(StatusCodes.Status404NotFound)
            .WithSummary("Retirar dinero")
            .WithDescription("Dado un número de tarjeta y un monto, permite realizar una extraccion. En caso de que el monto a retirar sea mayor" +
            " al saldo disponible en la tarjeta, retornara un codigo de error")
            .WithOpenApi();

            app.MapGet("/api/operaciones/{tarjeta}/{pagina}", async (IMediator mediator, int tarjeta, int pagina) =>
            {
                var operaciones = new ObtenerOperaciones { NroTarjeta = tarjeta, Pagina = pagina };
                var result = await mediator.Send(operaciones);
                if (result.IsError)
                {
                    return Results.Problem(result.Message,null, StatusCodes.Status404NotFound);
                }
                else
                {
                    return Results.Ok(result);
                }
            }).RequireAuthorization()
            .Produces<ServiceResult<List<Operacion>>>(StatusCodes.Status200OK)
            .Produces<ServiceResult<List<Operacion>>>(StatusCodes.Status404NotFound)
            .WithSummary("Obtener movimientos")
            .WithDescription("Dado un número de tarjeta retorna el historial de todas las operaciones realizadas. La respuesta se encuentra paginada " +
            "cada 10 registros")
            .WithOpenApi();

        }
    }
}
