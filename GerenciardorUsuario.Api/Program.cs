using GerenciadorUsuario.Api.Filters;
using GerenciadorUsuario.Api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "usuarios-api",
        ValidAudience = "usuarios-api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bXlT4X2lallYaJldEtleUFsYW5nMjAyNCMh")),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("buscar-por-id", policy => policy.RequireClaim("ler-dados-por-id"));
});
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
