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
        var parcelaService = A.Fake<ParcelaServices>();
        A.CallTo(() => parcelaService.GetListaParcelas(produtoTeste, condicaoTeste)).Returns(parcelasTeste);
    }

    [Theory]
    [InlineData(1000.00, 2)]
    [InlineData(1000.00, 4)]
    [InlineData(1000.00, 10)]
    [InlineData(2000.00, 4)]
    [InlineData(2000.00, 8)]
    [InlineData(2000.00, 20)]
    public void ListarParcelas(int valorProduto, int qtdeParcelas)
    {
        var parcelaService = A.Fake<ParcelaServices>();
        A.CallTo(() => parcelaService.GetListaParcelas(produtoTeste, condicaoTeste));

        parcelaService = new ParcelaServices();
        var produto = new Produto { Codigo = 1, Nome = "Produto", Valor = valorProduto };
        var condicao = new CondicaoPagamento { Valor = valorProduto/qtdeParcelas, QtdeParcelas = qtdeParcelas };

        var resultados = parcelaService.GetListaParcelas(produto, condicao);

        List<bool> isResultCorrect = new List<bool>();

        foreach(var resultado in resultados)
        {
            if (qtdeParcelas > 6)
            {
                isResultCorrect.Add(resultado.Valor == produto.Valor/qtdeParcelas + produto.Valor * 0.0115); 
            } 
            else {
                isResultCorrect.Add(resultado.Valor == produto.Valor/qtdeParcelas);
            }
        }

        Assert.True(isResultCorrect.All(t => t));
    }
}