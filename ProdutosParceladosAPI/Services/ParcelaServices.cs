using System.Collections.Concurrent;
using ProdutosParceladosAPI.Models;

namespace ProdutosParceladosAPI.Services;

public class ParcelaServices
{
    public virtual ConcurrentBag<Parcela> GetListaParcelas(Produto produto, CondicaoPagamento condicao)
    {
        ConcurrentBag<Parcela> parcelas = new ConcurrentBag<Parcela>();

        double valorResultante = produto.Valor - condicao.Valor;
        double valorDeParcela = valorResultante / condicao.QtdeParcelas;
        double taxaJuros = condicao.QtdeParcelas > 6 ? 0.0115 : 0;
        
        Parallel.For(0, condicao.QtdeParcelas, Func =>
        {
            parcelas.Add(new Parcela
            {
                NumeroParcela = Func + 1,
                Valor = valorDeParcela + valorResultante * taxaJuros,
                TaxaJurosAoMes = taxaJuros * 100
            });
        });

        return parcelas;
    }
}
