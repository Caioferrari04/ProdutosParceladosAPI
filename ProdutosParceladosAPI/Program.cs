using Microsoft.OpenApi.Models;
using ProdutosParceladosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo { 
                        Title = "ProdutosParceladosAPI", 
                        Version = "v1",
                        Description = "Teste de back-end da Via Varejo",
                        Contact = new OpenApiContact
                        {
                            Name = "Caio Silva Ferrari",
                            Email = "caioferrari0484@gmail.com",
                            Url = new Uri("https://github.com/Caioferrari04")
                        }
                    });
            });

builder.Services.AddScoped<ParcelaServices>();

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
