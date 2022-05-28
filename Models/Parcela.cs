namespace ProdutosParceladosAPI.Models;

public class Parcela : BaseModel
{
    public int NumeroParcela { 
        get => NumeroParcela; 
        set => NumeroParcela = isValueValid(value, "Numero de parcelas nao pode ser negativo");  
    }

    public double TaxaJurosAoMes { 
        get => TaxaJurosAoMes; 
        set => TaxaJurosAoMes = isValueValid(value, "Taxa de juros nao pode ser negativa"); 
    }
}
