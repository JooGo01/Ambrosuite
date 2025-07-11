using Ambrosuite.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using AutoMapper;
using Ambrosuite.ApiService.Mappers;
using Ambrosuite.ApiService.Interfaces;
using Ambrosuite.ApiService.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
/*
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // Puerto para HTTP
    serverOptions.ListenAnyIP(5001, listenOptions =>  // Puerto para HTTPS
    {
        listenOptions.UseHttps();
    });
});
*/

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    builder.WebHost.ConfigureKestrel(serverOptions =>
//    {
//        serverOptions.Listen(System.Net.IPAddress.Any, 5000); // HTTP interno
//    });
//});

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:2215")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var configuration = builder.Configuration;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
    };
});

builder.Services.AddAuthorization();

//Configuración Swagger 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors("AllowAll");

//app.UseHttpsRedirection();
/*
app.Use(async (context, next) =>
{
    if (!context.Request.IsHttps)
    {
        var withHttps = $"https://{context.Request.Host.Host}:5001{context.Request.Path}{context.Request.QueryString}";
        context.Response.Redirect(withHttps);
    }
    else
    {
        await next();
    }
});
*/

app.UseAuthorization();


app.MapControllers();

//Inicia swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ambrosuite API");
    c.RoutePrefix = string.Empty;
    });

app.Run();