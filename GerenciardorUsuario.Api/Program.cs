using GerenciadorUsuario.Api.Filters;
using GerenciadorUsuario.Api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddMemoryCache();
builder.Services.AddRateLimiter(_ =>
{
    _.AddFixedWindowLimiter("janela-fixa", options =>
    {
        options.QueueLimit = 5;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.PermitLimit = 2;
        options.Window = TimeSpan.FromSeconds(5);
    });
});
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
builder.Services.AddSwaggerGen(options =>
{
    var documentacao = new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "usuarioadmin@gmail.com",
            Name = "Esquipe API",
            Url = new Uri("http://www.google.com.br")
        },
        Description = "API para gerenciamento de usuários",
        Title = "API - Gerenciador de Usuários",
    };

    options.SwaggerDoc("v1", documentacao);
    options.SwaggerDoc("v2", documentacao);
});
builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configurar o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
    });
}

app.UseRateLimiter();
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
