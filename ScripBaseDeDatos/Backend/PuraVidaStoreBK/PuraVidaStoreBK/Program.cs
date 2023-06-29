using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

//Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("C:\\_LogsPuraVidaStore\\ApiLog-.txt", rollingInterval: RollingInterval.Day)
     .CreateLogger();

//dbcontex
var configuracion = builder.Configuration;
Estaticas.SqlServerConexcion = configuracion.GetConnectionString("sqlServer");
builder.Services.AddDbContext<PuraVidaStoreContext>(options =>
{
    options.UseSqlServer(configuracion.GetConnectionString("sqlServer"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#region Inyeccion de dependencias
builder.Services.AddScoped<IDataBase, DataBase>();
builder.Services.AddScoped<IBodegaQuery, BodegaQuery>();
builder.Services.AddScoped<IMovimientosQuery, MovimientosQuery>();
builder.Services.AddScoped<IMayoristaQuery, MayoristaQuery>();
builder.Services.AddScoped<IPersonaQuery, PersonaQuery>();
builder.Services.AddScoped<IProductoQuery, ProductoQuery>();
builder.Services.AddScoped<IUsuariosQuerys,UsuariosQuerys>();
builder.Services.AddScoped<IRolesQuerys, RolesQuerys>();
builder.Services.AddScoped<ITipoProductoQuery, TipoProductoQuery>();
builder.Services.AddScoped<IVentasQuery, VentasQuery>();
builder.Services.AddScoped<ICorreoQuery, CorreoQuery>();
builder.Services.AddScoped<IParametrosGeneralesQuery, ParametrosGeneralesQuery>();
builder.Services.AddScoped<IImpuestosQuery, ImpuestosQuery>();
builder.Services.AddScoped<IEnvioCorreo, EnvioCorreo>();



#endregion



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

