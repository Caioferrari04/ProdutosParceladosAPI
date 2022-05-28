using Microsoft.AspNetCore.Mvc;
using ProdutosParceladosAPI.API;
using ProdutosParceladosAPI.Models;
using ProdutosParceladosAPI.Services;

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
        return Ok(_parcelaServices.GetListaParcelas(request.produto, request.condicaoPagamento));
    }
}