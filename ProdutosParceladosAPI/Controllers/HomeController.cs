using Microsoft.AspNetCore.Mvc;
using ProdutosParceladosAPI.API;
using ProdutosParceladosAPI.Models;
using ProdutosParceladosAPI.Services;
using ProdutosParceladosAPI.Validation;

namespace ProdutosParceladosAPI.Controllers;

[Route("/")]
public class HomeController : Controller 
{
    private readonly ParcelaServices _parcelaServices;
    public HomeController(ParcelaServices parcelaServices)
    {
        _parcelaServices = parcelaServices;
    }

    [HttpGet]
    public IActionResult Index([FromBody]RequestModel request) 
    {
        var produto = request.produto;
        var condicao = request.condicaoPagamento;

        var produtoValidator = new ProdutoValidator();
        var result = produtoValidator.Validate(produto);

        if(result.IsValid)
            return Ok(_parcelaServices.GetListaParcelas(produto, condicao));
            
        return BadRequest("Valor nao pode ser negativo");
    }
}