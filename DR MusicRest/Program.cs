using DR_MusicRest.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow requests from any origin, method, and header
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy
                                  .AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});





// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



// Dependency Injection: Register the SongsRepoList as the implementation for ISongsRepo
builder.Services.AddSingleton<ISongsRepo, SongsRepoList>();


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable CORS using the defined policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
