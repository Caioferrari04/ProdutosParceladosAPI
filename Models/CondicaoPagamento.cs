using System.ComponentModel;

namespace ProdutosParceladosAPI.Models;

public class CondicaoPagamento : BaseModel
{
    [DisplayName("ValorEntrada")]
    public override double Valor { get => base.Valor; set => base.Valor = value; }

    public int QtdeParcelas { 
        get => QtdeParcelas; 
        set => QtdeParcelas = isValueValid(value, "Quantidade de parcelas nao podem ser negativas"); 
    }
}