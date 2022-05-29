using ProdutosParceladosAPI.Models;
using ProdutosParceladosAPI.Services;
namespace ProdutosParceladosTests;

public class ParcelaServiceTest
{
    [Fact]
    public void ListaParcelasSemValorEntrada()
    {
        var parcelaService = new ParcelaServices();
        var produto = new Produto() { Codigo = 1, Nome = "Produto", Valor = 1000.00 };
        var condicao = new CondicaoPagamento() { Valor = 0, QtdeParcelas = 10 };

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        Assert.True(resultados.All(t => t.Valor == produto.Valor / condicao.QtdeParcelas + produto.Valor * 0.0115));
    }

    [Fact]
    public void ListaParcelasComValorEntrada()
    {
        var parcelaService = new ParcelaServices();
        var produto = new Produto() { Codigo = 1, Nome = "Produto", Valor = 1000.00 };
        var condicao = new CondicaoPagamento() { Valor = 200, QtdeParcelas = 10 };

        var valorResultante = produto.Valor - condicao.Valor;

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        Assert.True(resultados.All(t => t.Valor == valorResultante / condicao.QtdeParcelas + valorResultante * 0.0115));
    }

    [Theory]
    [InlineData(1000.00, 4)]
    [InlineData(1000.00, 10)]
    [InlineData(2000.00, 8)]
    [InlineData(2000.00, 20)]
    public void ListarParcelasSemValorEntrada(double valorProduto, int qtdeParcelas)
    {
        var parcelaService = new ParcelaServices();
        var produto = new Produto { Codigo = 1, Nome = "Produto", Valor = valorProduto };
        var condicao = new CondicaoPagamento { Valor = 0, QtdeParcelas = qtdeParcelas };

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        double taxaJuros = qtdeParcelas > 6 ? 0.0115 : 0;

        Assert.True(resultados.All(t => t.Valor == produto.Valor / condicao.QtdeParcelas + produto.Valor * taxaJuros));
    }

    [Theory]
    [InlineData(1000, 200, 10)]
    [InlineData(1000, 1000, 1)]
    [InlineData(2000, 200, 20)]
    [InlineData(2000, 1000, 2)]
    public void ListarParcelasComValorEntrada(double valorProduto, double valorEntrada, int qtdeParcelas) 
    {
        var parcelaService = new ParcelaServices();
        var produto = new Produto { Codigo = 1, Nome = "Produto", Valor = valorProduto };
        var condicao = new CondicaoPagamento { Valor = 0, QtdeParcelas = qtdeParcelas };

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        var valorResultante = produto.Valor - condicao.Valor;

        double taxaJuros = qtdeParcelas > 6 ? 0.0115 : 0;

        Assert.True(resultados.All(t => t.Valor == valorResultante / condicao.QtdeParcelas + valorResultante * taxaJuros));
    }
}
