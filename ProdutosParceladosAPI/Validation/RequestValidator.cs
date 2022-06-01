using FluentValidation;
using ProdutosParceladosAPI.API;

namespace ProdutosParceladosAPI.Validation;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(p => p.produto.Valor)
            .GreaterThan(0.0).WithMessage("Valor nao pode ser negativo ou zero");

        RuleFor(p => p.produto.Codigo)
            .GreaterThan(0.0).WithMessage("Codigo nao pode ser negativo ou zero");

        RuleFor(p => p.produto.Nome)
            .NotNull().WithMessage("Insira um nome")
            .NotEmpty().WithMessage("Nome nao pode estar vazio");

        RuleFor(c => c.condicaoPagamento.QtdeParcelas)
            .GreaterThan(0)
            .WithMessage("Quantidade nao pode ser zero ou negativa");

        RuleFor(c => c.condicaoPagamento.QtdeParcelas)
            .LessThanOrEqualTo(1)
            .When(p => p.produto.Valor - p.condicaoPagamento.Valor == 0)
            .WithMessage("Produto pago a vista, nao e possivel parcelar mais de uma vez");

        RuleFor(c => c.condicaoPagamento.Valor)
            .GreaterThanOrEqualTo(0).WithMessage("Valor de entrada nao pode ser negativo")
            .LessThanOrEqualTo(c => c.produto.Valor).WithMessage("Valor de entrada nao pode ser maior que valor de produto");
    }
}