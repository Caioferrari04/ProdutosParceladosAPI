using System.Collections.Concurrent;
using ProdutosParceladosAPI.API;
using ProdutosParceladosAPI.Models;
using RestSharp;
using Newtonsoft.Json;

namespace ProdutosParceladosAPI.Services;

public class ParcelaServices
{
    public virtual async Task<ConcurrentBag<Parcela>> GetListaParcelas(Produto produto, CondicaoPagamento condicao)
    {
        ConcurrentBag<Parcela> parcelas = new ConcurrentBag<Parcela>();

        var resultado = await _getValorSelic();

        double taxaJuros = condicao.QtdeParcelas > 6 ? resultado.valor : 0;
        
        double valorResultante = produto.Valor - condicao.Valor;
        double valorParcela = valorResultante / condicao.QtdeParcelas;

        valorParcela = valorResultante == 0 ? 1000 : valorParcela;
        valorParcela = Math.Round(valorParcela + valorResultante * taxaJuros, 2);

        taxaJuros *= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        
        Parallel.For(0, condicao.QtdeParcelas, new ParallelOptions { MaxDegreeOfParallelism = 4 }, Func =>
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

    private async Task<Response> _getValorSelic() 
    {
        var client = new RestClient($"http://api.bcb.gov.br/dados");
        var request = new RestRequest("/serie/bcdata.sgs.11/dados/ultimos/1?formato=json");
        var response = await client.ExecuteAsync(request);
        
        var correto = response.Content.Remove(0,1).Remove(response.Content.Count()-2);
        return JsonConvert.DeserializeObject<Response>(correto);
    }
}
