using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace challenge_metafar.Extensions
{
    public static class JWTExtension
    {
        public static void RegisterJWT(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]));
                var signingCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = signinKey,
                };
            });
        }
    }
}
