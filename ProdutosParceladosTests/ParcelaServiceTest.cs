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
            new Parcela { NumeroParcela = 1, Valor = 505.75, TaxaJurosAoMes = 1.15 },
            new Parcela { NumeroParcela = 2, Valor = 505.75, TaxaJurosAoMes = 1.15 }
        };
    }

    [Fact]
    public void ListaParcelas()
    {
        var parcelaService = A.Fake<ParcelaServices>();
        A.CallTo(() => parcelaService.GetListaParcelas(produtoTeste, condicaoTeste)).Returns(parcelasTeste);
    }
}