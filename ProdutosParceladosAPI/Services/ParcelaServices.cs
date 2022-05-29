using System.Collections.Concurrent;
using ProdutosParceladosAPI.Models;

namespace ProdutosParceladosAPI.Services;

public class ParcelaServices
{
    public virtual ConcurrentBag<Parcela> GetListaParcelas(Produto produto, CondicaoPagamento condicao)
    {
        ConcurrentBag<Parcela> parcelas = new ConcurrentBag<Parcela>();

        double valorResultante = produto.Valor - condicao.Valor;
        double valorParcela = valorResultante / condicao.QtdeParcelas;
        double taxaJuros = condicao.QtdeParcelas > 6 ? 0.0115 : 0;

        valorParcela = valorResultante == 0 ? 1000 : valorParcela;
        valorParcela = valorParcela + valorResultante * taxaJuros;
        taxaJuros *= 100;
        
        Parallel.For(0, condicao.QtdeParcelas, Func =>
        {
            parcelas.Add(new Parcela
            {
                NumeroParcela = Func + 1,
                Valor = valorParcela,
                TaxaJurosAoMes = taxaJuros
            });
        });

        return parcelas;
    }
}
