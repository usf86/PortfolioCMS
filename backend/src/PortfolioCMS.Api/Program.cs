using Microsoft.EntityFrameworkCore;
using PortfolioCMS.Application.Services;
using PortfolioCMS.Infrastructure.Persistence;
using PortfolioCMS.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// ---------- Services ----------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PortfolioCMS API",
        Version = "v1",
        Description = "API generique pour sites portfolio configurables (boulangerie, garage, boucherie...)."
    });
});

// Connexion MySQL via Pomelo (EF Core Code-First)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Chaine de connexion 'DefaultConnection' manquante dans appsettings.json");

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Injection de dependances - couche Application
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

// CORS pour le front Angular (dev local)
const string AngularCorsPolicy = "AllowAngularApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularCorsPolicy, policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ---------- Seed automatique en developpement ----------
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    await SeedData.SeedAsync(dbContext);
}

// ---------- Middlewares ----------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AngularCorsPolicy);
app.UseAuthorization();
app.MapControllers();

app.Run();
