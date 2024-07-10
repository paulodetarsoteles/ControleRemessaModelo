using ControleRemessaModelo.API.Injectors;
using ControleRemessaModelo.API.Services;
using ControleRemessaModelo.API.Utils;
using ControleRemessaModelo.Negocio.Helpers;
using ControleRemessaModelo.Repositorio.DataConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;
using SQLitePCL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region SQLite
Batteries.Init();
#endregion

#region Injeções de Dependência
AutenticacaoInjector.Injector(builder);
ServicoInjector.Injector(builder);
RepositorioInjector.Injector(builder);
#endregion

#region Configuração dos Logs (Serilog))
var logger = new LoggerConfiguration()
        .MinimumLevel.Error()
        .WriteTo.File("Logs/log-api.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion

builder.Services.AddAutoMapper(typeof(AutoMappings).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ConnectionSetting>(builder.Configuration.GetSection("ConnectionStrings"));

#region Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ControleRemessaModelo.API",
        Description = "Swagger de controle de reuisições de endpoints"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Cabeçalho de autorização JWT está usando o esquema de Bearer Token \r\n " +
                      "Obs.: Digite o 'Bearer' antes do token'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });

});
#endregion

#region Autenticação JWT
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationFile.GetConfigurationKey("SecretKey"))),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.MapControllers();

app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
