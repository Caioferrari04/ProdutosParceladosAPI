using System.Collections.Concurrent;
using ProdutosParceladosAPI.Models;

namespace ProdutosParceladosAPI.Services;

public class ParcelaServices
{
    public virtual ConcurrentBag<Parcela> GetListaParcelas(Produto produto, CondicaoPagamento condicao)
    {
        ConcurrentBag<Parcela> parcelas = new ConcurrentBag<Parcela>();
        if (condicao.QtdeParcelas > 6)
        {
            parcelas.Add(new Parcela { NumeroParcela = 1, Valor = condicao.Valor + condicao.Valor * 0.0115, TaxaJurosAoMes = 1.15 });
            Parallel.For(2, (int)condicao.QtdeParcelas, Func =>
            {
                parcelas.Add(new Parcela
                {
                    NumeroParcela = Func,
                    Valor = produto.Valor + (produto.Valor / condicao.QtdeParcelas) * 0.0115,
                    TaxaJurosAoMes = 1.15
                });
            });
        }
        else
        {
            Parallel.For(0, (int)condicao.QtdeParcelas, Func =>
            {
                parcelas.Add(new Parcela
                {
                    NumeroParcela = Func + 1,
                    Valor = produto.Valor
                });
            });
        }
        return parcelas;
    }
}