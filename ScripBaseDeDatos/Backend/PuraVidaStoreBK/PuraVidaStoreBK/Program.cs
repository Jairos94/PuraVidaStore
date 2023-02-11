using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Utilitarios;
using PuraVidaStoreBK.Utilitarios.Interfase;
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

//Promethius
builder.Services.AddHttpClient(Options.DefaultName)
    .UseHttpClientMetrics();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var conexcion = builder.Configuration.GetConnectionString("sqlServer");

//dbcontex
Estaticas.SqlServerConexcion = builder.Configuration.GetConnectionString("sqlServer");

#region Inyeccion de dependencias
builder.Services.AddSingleton<IDataBase, DataBase>();
builder.Services.AddSingleton<IBodegaQuery, BodegaQuery>();
builder.Services.AddSingleton<IMovimientosQuery, MovimientosQuery>();
builder.Services.AddSingleton<IMayoristaQuery, MayoristaQuery>();
builder.Services.AddSingleton<IPersonaQuery, PersonaQuery>();
builder.Services.AddSingleton<IProductoQuery, ProductoQuery>();
builder.Services.AddSingleton<IUsuariosQuerys,UsuariosQuerys>();
builder.Services.AddSingleton<IRolQuery, RolesQuerys>();
builder.Services.AddSingleton<ITipoProductoQuery, TipoProductoQuery>();
builder.Services.AddSingleton<IVentasQuery, VentasQuery>();
builder.Services.AddSingleton<ICorreoQuery, CorreoQuery>();
builder.Services.AddSingleton<IParametrosGeneralesQuery, ParametrosGeneralesQuery>();
builder.Services.AddSingleton<IImpuestosQuery, ImpuestosQuery>();


builder.Services.AddSingleton<IEnvioCorreo, EnvioCorreo>();



#endregion

//Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration["Serilog:seq"])
    .WriteTo.File("C:\\_LogsPuraVidaStore\\ApiLog-.txt", rollingInterval:RollingInterval.Day)
     .CreateLogger();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");


app.UseHttpMetrics(options => 
{
    options.ReduceStatusCodeCardinality();
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});
app.UseGrpcMetrics();

app.Run();

