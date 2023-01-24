using System.Text;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using training.Model;
using training.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var defaultCredentials = new DefaultAzureCredential();
var keyVaultEndpoint = builder.Configuration["AzureKeyVaultEndpoint"];

//builder.AddAzureKeyVault(new Uri(keyVaultEndpoint));
//builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), defaultCredentials,
//    new AzureKeyVaultConfigurationOptions
//    {
//        // Manager = new PrefixKeyVaultSecretManager(secretPrefix),
//        ReloadInterval = TimeSpan.FromMinutes(5)
//    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biodata API", Version = "v1"});

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description="Standard Authorization using bearer scheme (\"bearer {token}\" ) ",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddControllers();

//builder.Services.AddSingleton<SecretManagerService>();

//builder.Services.AddDbContext<studentDbContext>();

builder.Services.AddDbContext<studentDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
    options.UseMySql("Server=sp-dot-net.c4upicmbdujn.ap-south-1.rds.amazonaws.com;User=admin;Password=DotNet123;Database=studentCRUD;", serverVersion);
});


builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("my favourite token is here thank you")),
          ValidateIssuer=false,
          ValidateAudience=false
        };
    });
var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biodata API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

