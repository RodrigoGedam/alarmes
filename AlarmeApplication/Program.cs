using AlarmeApplication.Data.Contexts;
using AlarmeApplication.Data.Repository;
using AlarmeApplication.Models;
using AlarmeApplication.Services;
using AlarmeApplication.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configuração do DB

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true));

#endregion

#region Repositories
builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
#endregion

#region Services
builder.Services.AddScoped<IOcorrenciaService, OcorrenciaService>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(
    c =>
    {
        c.AllowNullCollections = true;
        c.AllowNullDestinationValues = true;

        c.CreateMap<OcorrenciaModel, OcorrenciaViewModel>();

        c.CreateMap<OcorrenciaViewModel, OcorrenciaModel>();


    }
);

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
#endregion

#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+x&uBMQgwPju6yzyePi")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
