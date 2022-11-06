using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme 
    {
        Description="Standar Authorization header using the Bearer scheme (\"bearer {token}\")",
        In= ParameterLocation.Header,
        Name="Authorization",
        Type= SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#region Cors
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
#endregion


//Jw Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:token").Value)),
            ValidateIssuer = false,
            ValidateAudience=false
        };
    });
    

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

#region Inyeccion de dependencias
builder.Services.AddSingleton<IUsuariosQuerys,UsuariosQuerys>();
#endregion

//Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("C:\\_LogsPuraVidaStore\\ApiLog-.txt", rollingInterval:RollingInterval.Day).CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

