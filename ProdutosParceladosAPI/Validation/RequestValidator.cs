using FluentValidation;
using ProdutosParceladosAPI.API;

namespace ProdutosParceladosAPI.Validation;

public class RequestValidator : AbstractValidator<RequestModel>
{
    public RequestValidator()
    {
        RuleFor(p => p.produto.Valor)
            .GreaterThan(0.0).WithMessage("Valor nao pode ser negativo");

        RuleFor(p => p.produto.Codigo)
            .GreaterThan(0.0).WithMessage("Codigo nao pode ser negativo");

        RuleFor(p => p.produto.Nome)
            .NotNull().WithMessage("Insira um nome")
            .NotEmpty().WithMessage("Nome nao pode estar vazio");

        RuleFor(c => c.condicaoPagamento.QtdeParcelas)
            .GreaterThan(0)
            .WithMessage("Quantidade nao pode ser negativa");

        RuleFor(c => c.condicaoPagamento.Valor)
            .GreaterThan(0).WithMessage("Valor de entrada nao pode ser negativo")
            .LessThanOrEqualTo(c => c.produto.Valor).WithMessage("Valor de entrada nao pode ser maior que valor de produto");
    }
}