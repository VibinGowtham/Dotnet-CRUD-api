using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using training.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biodata API", Version = "v1" });
});


builder.Services.AddControllers();

builder.Services.AddScoped<studentDbContext>();


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

