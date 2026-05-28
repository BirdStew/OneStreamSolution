var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add HttpClient for BackendAPI1
builder.Services.AddHttpClient("BackendAPI1", client =>
{
    client.BaseAddress = new Uri("https://localhost:7019"); 
});

// Add HttpClient for BackendAPI2
builder.Services.AddHttpClient("BackendAPI2", client =>
{
    client.BaseAddress = new Uri("https://localhost:7233"); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
