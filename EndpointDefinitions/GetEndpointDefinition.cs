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

namespace challenge_metafar.EndpointDefinitions
{
    public class GetEndpointDefinition : IEndpointDefinition
    {

        
        public void RegisterEndpoints(WebApplication app, WebApplicationBuilder builder)
        {

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
                    return Results.BadRequest(resultLogin);
                }
               
                
            });

            app.MapGet("/api/getSaldo/{tarjeta}", async (IMediator mediator, int tarjeta) =>
            {
                var obtenerTarjeta = new ObtenerSaldoPorNroTarjeta { tarjeta = tarjeta };
                var result = await mediator.Send(obtenerTarjeta);
                Console.WriteLine(result);
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapGet("/api/retiro/{tarjeta}/{monto}", async (IMediator mediator, int tarjeta, decimal monto) =>
            {
                var obtenerTarjeta = new ObtenerSaldoPorNroTarjeta { tarjeta = tarjeta };
                var result = await mediator.Send(obtenerTarjeta);
                Console.WriteLine(result);
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
