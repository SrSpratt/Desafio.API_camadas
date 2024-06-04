using Desafio.Domain.Setup;
using Desafio.Infrastructure.Repository;
using Desafio.Services.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adicionar repositório {deixarei o singleton enquanto for mockado}
builder.Services.AddSingleton<IRepository, ProductRepository>();

//adicionar serviços
builder.Services.AddScoped<IService, ProductService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

//dá um bind no json com a classe api config
builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection(nameof(ApiConfig)));

builder.Services.AddSingleton<IApiConfig>(x => x.GetRequiredService<IOptions<ApiConfig>>().Value);

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
