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
            parcelas.Add(new Parcela { NumeroParcela = 1, Valor = condicao.Valor + produto.Valor * 0.0115 , TaxaJurosAoMes = 1.15 });

            double valorFaltante = produto.Valor - condicao.Valor;
            double valorDeParcela = produto.Valor / condicao.QtdeParcelas;
            double valorResultante = produto.Valor - valorDeParcela == valorFaltante ? produto.Valor : produto.Valor - (condicao.Valor - valorDeParcela);
            int qtdeParcelasReal = condicao.QtdeParcelas - 1;
            valorDeParcela = valorDeParcela - ((condicao.Valor - valorDeParcela) / qtdeParcelasReal);

            Parallel.For(1, condicao.QtdeParcelas, Func =>
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
                double valorFaltante = produto.Valor - condicao.Valor;
                double valorDeParcela = produto.Valor / condicao.QtdeParcelas;
                double valorResultante = produto.Valor - valorDeParcela == valorFaltante ? produto.Valor : valorFaltante;

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