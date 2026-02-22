using PopayanFilms.Modules.Movies.Api.Endpoints;
using PopayanFilms.Modules.Movies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel URLs (fallback if not using launchSettings.json)
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("http://localhost:5001", "https://localhost:7001");
}

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(PopayanFilms.Modules.Movies.Api.Controllers.MoviesController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Popayan Films API",
        Version = "v1",
        Description = "API para gestión de películas - Clean Architecture con Monolito Modular"
    });
});

// Add Modules
builder.Services.AddMoviesModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Popayan Films API v1");
        options.RoutePrefix = "swagger";
    });
}

// Map Controllers
app.MapControllers();

// Map Module Endpoints (Minimal APIs - opcional, puedes eliminar si solo usas controladores)
app.MapMoviesEndpoints();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }))
    .WithTags("Health");

app.Run();
