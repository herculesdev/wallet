using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Wallet.Domain.Helpers;
using Wallet.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wallet API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
    { 
        Name = "Authorization", 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
        In = ParameterLocation.Header, 
        Description = "JWT Authorization header using the Bearer scheme. Please, enter \"bearer your_token\"", 
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    { 
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            }, 
            new string[] {} 
        } 
    }); 
});
builder.Services.ConfigureWallet(builder.Configuration);
builder.Services.AddMediatR(Utils.GetAssembly());
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Utils.TokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();