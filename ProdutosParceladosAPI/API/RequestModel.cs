using ProdutosParceladosAPI.Models;

namespace ProdutosParceladosAPI.API;

public class RequestModel
{
    public Produto produto { get; set; }
    public CondicaoPagamento condicaoPagamento { get; set; }
}