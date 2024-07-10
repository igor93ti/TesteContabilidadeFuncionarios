using ContabilidadeFuncionarios.API.Configurations;
using ContabilidadeFuncionarios.Application;
using ContabilidadeFuncionarios.Infrastructure;
using ContabilidadeFuncionarios.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contabilidade Funcionários", Version = "v1" });
    c.EnableAnnotations();
});

var jwtSettingsSection = builder.Configuration.GetSection("Jwt");
var jwtSettings = new JwtSettings(
    Key: jwtSettingsSection["Key"] ?? throw new ArgumentNullException(nameof(jwtSettingsSection), "JWT Key não está configurado"),
    Issuer: jwtSettingsSection["Issuer"] ?? throw new ArgumentNullException(nameof(jwtSettingsSection), "JWT Issuer não está configurado"),
    Audience: jwtSettingsSection["Audience"] ?? throw new ArgumentNullException(nameof(jwtSettingsSection), "JWT Audience não está configurado"),
    ExpiryMinutes: int.TryParse(jwtSettingsSection["ExpiryMinutes"], out var expiryMinutes) ? expiryMinutes : throw new ArgumentNullException(nameof(jwtSettingsSection), "JWT ExpiryMinutes não está configurado")
);
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        SeedData.Initialize(dbContext);
    }
}

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate();

//    SeedData.Initialize(dbContext);
//}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();