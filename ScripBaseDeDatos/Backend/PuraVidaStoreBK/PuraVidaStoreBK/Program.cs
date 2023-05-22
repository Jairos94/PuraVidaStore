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
Log.Information(Estaticas.SqlServerConexcion);
builder.Services.AddDbContext<PuraVidaStoreContext>(options =>
{
    options.UseSqlServer(configuracion.GetConnectionString("sqlServer"), sqlServerOptions =>
    {
       // sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});

#region Inyeccion de dependencias
builder.Services.AddTransient<IDataBase, DataBase>();
builder.Services.AddTransient<IBodegaQuery, BodegaQuery>();
builder.Services.AddTransient<IMovimientosQuery, MovimientosQuery>();
builder.Services.AddTransient<IMayoristaQuery, MayoristaQuery>();
builder.Services.AddTransient<IPersonaQuery, PersonaQuery>();
builder.Services.AddTransient<IProductoQuery, ProductoQuery>();
builder.Services.AddTransient<IUsuariosQuerys,UsuariosQuerys>();
builder.Services.AddTransient<IRolesQuerys, RolesQuerys>();
builder.Services.AddTransient<ITipoProductoQuery, TipoProductoQuery>();
builder.Services.AddTransient<IVentasQuery, VentasQuery>();
builder.Services.AddTransient<ICorreoQuery, CorreoQuery>();
builder.Services.AddTransient<IParametrosGeneralesQuery, ParametrosGeneralesQuery>();
builder.Services.AddTransient<IImpuestosQuery, ImpuestosQuery>();


builder.Services.AddTransient<IEnvioCorreo, EnvioCorreo>();



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

