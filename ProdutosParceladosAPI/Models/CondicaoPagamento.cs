using System.ComponentModel;

namespace ProdutosParceladosAPI.Models;

public class CondicaoPagamento : BaseModel
{
    [DisplayName("ValorEntrada")]
    public override double Valor { get => base.Valor; set => base.Valor = value; }

    public int QtdeParcelas { get; set;}
}