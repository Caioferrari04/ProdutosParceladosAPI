using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ProdutosParceladosAPI.Models;

public class CondicaoPagamento : BaseModel
{
    [JsonPropertyName("valorEntrada")]
    public override double Valor { get => base.Valor; set => base.Valor = value; }

    public int QtdeParcelas { get; set;}
}