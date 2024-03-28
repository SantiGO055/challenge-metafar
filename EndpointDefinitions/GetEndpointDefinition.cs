using Application.Movimiento.Queries;
using challenge_metafar.Abstractions;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace challenge_metafar.EndpointDefinitions
{
    public class GetEndpointDefinition : IEndpointDefinition
    {


        public void RegisterEndpoints(WebApplication app, WebApplicationBuilder builder)
        {
            app.MapGet("/api/auth/{tarjeta}/{pin}", async (IMediator mediator, int tarjeta, string pin) =>
            {


                // llamar a la base para validar datos


                if (tarjeta == 123456 && pin == "1234")
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

                    return tokenHandler.WriteToken(token);
                }
                else
                {
                    return "Usuario invalidos";
                }
            });

            app.MapGet("/api/getSaldo/{tarjeta}", async (IMediator mediator, int tarjeta) =>
            {
                var obtenerTarjeta = new ObtenerSaldoPorNroTarjeta { tarjeta = tarjeta };
                var result = await mediator.Send(obtenerTarjeta);
                Console.WriteLine(result);
                return result;
            }).RequireAuthorization().WithName("getSaldoByTarjeta");
        }
    }
}
