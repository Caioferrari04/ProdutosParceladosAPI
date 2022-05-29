using FluentValidation;
using ProdutosParceladosAPI.Models;

namespace ProdutosParceladosAPI.Validation;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(p => p.Valor).GreaterThan(0.0);
        RuleFor(p => p.Codigo).GreaterThan(0.0);
        RuleFor(p => p.Nome).NotNull().NotEmpty();
    }
}
