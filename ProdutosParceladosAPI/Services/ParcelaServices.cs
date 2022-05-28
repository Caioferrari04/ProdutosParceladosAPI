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
        if (condicao.QtdeParcelas > 6)
        {
            Parallel.For(0, condicao.QtdeParcelas, Func =>
            {
                parcelas.Add(new Parcela
                {
                    NumeroParcela = Func + 1,
                    Valor = valorDeParcela + valorResultante * 0.0115,
                    TaxaJurosAoMes = 1.15
                });
            });
        }
        else
        {
            Parallel.For(0, condicao.QtdeParcelas, Func =>
            {
                parcelas.Add(new Parcela
                {
                    NumeroParcela = Func + 1,
                    Valor = valorResultante / condicao.QtdeParcelas
                });
            });
        }
        return parcelas;
    }
}