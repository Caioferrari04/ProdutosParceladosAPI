namespace ProdutosParceladosAPI.Models;

public abstract class BaseModel 
{
    public virtual double Valor { 
        get => Valor; 
        set => Valor = isValueValid(value, "Valor nao pode ser negativo");
    }
    
    protected dynamic isValueValid(dynamic value, string errorMsg)
    {
        if (value < 0)
            throw new ApplicationException(errorMsg);
        return value;
    }
} 