namespace ProdutosParceladosAPI.Models;

public class Produto : BaseModel
{
    public int Codigo { 
        get => Codigo; 
        set => Codigo = isValueValid(value, "Codigo nao pode ser negativo"); 
    }

    public string Nome { 
        get => Nome; 
        set => Nome = Nome = string.IsNullOrWhiteSpace(value) ? value : throw new ApplicationException("Nome nao pode ser vazio"); 
    }
}