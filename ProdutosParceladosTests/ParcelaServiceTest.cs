using System.Collections.Concurrent;
using FakeItEasy;
using ProdutosParceladosAPI.Models;
using ProdutosParceladosAPI.Services;
namespace ProdutosParceladosTests;

public class ParcelaServiceTest
{
    Produto produtoTeste;
    CondicaoPagamento condicaoTeste;
    ConcurrentBag<Parcela> parcelasTeste;
    public ParcelaServiceTest()
    {
        produtoTeste = new Produto() { Codigo = 1, Nome = "Teste", Valor = 1000.00 };
        condicaoTeste = new CondicaoPagamento() { Valor = 500.00, QtdeParcelas = 2 };
        parcelasTeste = new ConcurrentBag<Parcela>() {
            new Parcela { NumeroParcela = 1, Valor = 500 },
            new Parcela { NumeroParcela = 2, Valor = 500 }
        };
    }

    [Fact]
    public void ListaParcelas()
    {
        var parcelaService = new ParcelaServices();
        var produto = new Produto() { Codigo = 1, Nome = "Produto", Valor = 1000.00 };
        var condicao = new CondicaoPagamento() { Valor = 0, QtdeParcelas = 10 };

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        Assert.True(resultados.All(t => t.Valor == produto.Valor/condicao.QtdeParcelas + produto.Valor * 0.0115));
    }

    [Theory]
    [InlineData(1000.00, 4)]
    [InlineData(1000.00, 10)]
    [InlineData(2000.00, 8)]
    [InlineData(2000.00, 20)]
    public void ListarParcelas(int valorProduto, int qtdeParcelas)
    {
        var parcelaService = new ParcelaServices();
        var produto = new Produto { Codigo = 1, Nome = "Produto", Valor = valorProduto };
        var condicao = new CondicaoPagamento { Valor = 0, QtdeParcelas = qtdeParcelas };

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        List<bool> isResultCorrect = new List<bool>();
        double taxaJuros = qtdeParcelas > 6 ? 0.0115 : 0;

        foreach(var resultado in resultados)
            isResultCorrect.Add(resultado.Valor == produto.Valor/qtdeParcelas + produto.Valor * taxaJuros); 

        Assert.True(isResultCorrect.All(t => t));
    }
}