using ProdutosParceladosAPI.Models;

namespace ProdutosParceladosAPI.API;

public class Request
{
    public Produto produto { get; set; }
    public CondicaoPagamento condicaoPagamento { get; set; }
}