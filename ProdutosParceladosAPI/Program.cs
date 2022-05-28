using ProdutosParceladosAPI.Models;
using ProdutosParceladosAPI.Services;

ParcelaServices servico = new ParcelaServices();

var condicaoTeste = new CondicaoPagamento() { Valor = 500.00, QtdeParcelas = 10 };
var produtoTeste = new Produto() { Codigo = 1, Nome = "Teste", Valor = 1000.00 };

foreach (var parcela in servico.GetListaParcelas(produtoTeste, condicaoTeste))
{
    System.Console.WriteLine(parcela.Valor);
}

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddScoped<ParcelaServices>();

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();
