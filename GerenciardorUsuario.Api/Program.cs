using GerenciadorUsuario.Api.Filters;
using GerenciadorUsuario.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Use((HttpContext, next) =>
{
    var logger = HttpContext.RequestServices.GetService<ILogger<Program>>();
    logger.LogInformation(
        "Requisição com o método {Metodo} para rota {Rota}",
        HttpContext.Request.Method,
        HttpContext.Request.Path);
    return next();
});

app.Run();
