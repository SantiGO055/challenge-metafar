using Application.Abstractions;
using Application.Movimiento.Commands;
using Application.Movimiento.Queries;
using challenge_metafar.Extensions;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
builder.RegisterJWT();

var app = builder.Build();
app.RegisterEdpointDefinitions(builder);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

