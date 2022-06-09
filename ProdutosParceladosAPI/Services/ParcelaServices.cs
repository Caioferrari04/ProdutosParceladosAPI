using System.Collections.Concurrent;
using ProdutosParceladosAPI.API;
using ProdutosParceladosAPI.Models;
using RestSharp;
using Newtonsoft.Json;

namespace ProdutosParceladosAPI.Services;

public class ParcelaServices
{
    public virtual async Task<Parcela[]> GetListaParcelas(Produto produto, CondicaoPagamento condicao)
    {
        Parcela[] parcelas = new Parcela[condicao.QtdeParcelas];

        var resultado = await _getValorSelic();

        double taxaJuros = condicao.QtdeParcelas > 6 ? resultado.valor : 0;
        
        double valorResultante = produto.Valor - condicao.Valor;
        double valorParcela = valorResultante / condicao.QtdeParcelas;

        valorParcela = valorResultante is 0 ? produto.Valor : valorParcela;
        valorParcela = Math.Round(valorParcela + valorResultante * taxaJuros, 2);

        var tempoAtual = DateTime.Now;

        if (taxaJuros is not 0) {
            taxaJuros *= DateTime.DaysInMonth(tempoAtual.Year, tempoAtual.Month);

            taxaJuros = Math.Round(taxaJuros, 2);    
        }

        for (int i = 0; i < condicao.QtdeParcelas; i++)
        {
            parcelas[i] = new Parcela() {
                NumeroParcela = i + 1,
                TaxaJurosAoMes = taxaJuros,
                Valor = valorParcela
            };
        }

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
