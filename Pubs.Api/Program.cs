
using Pubs.Api.Extensions;
using Pubs.Api.Filters;
using Pubs.Api.Middlewares;
using Pubs.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura os serviços.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura as extensões
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureCors();

// AutoMapper (mapeamento)
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configuração de Pipeline Htttp (Configura a documentação do Swagger).
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("AllowAngular");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicializa o BD
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PubsContext>();
        context.Database.EnsureCreated();
        Console.WriteLine("Database initialized successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao inicializar o banco de dados: {ex.Message}");
    }
}

app.Run();