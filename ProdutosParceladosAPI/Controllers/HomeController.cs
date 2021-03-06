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
    public async Task<IActionResult> Index([FromBody] Request request)
    {
        var requestValidator = new RequestValidator();
        var result = requestValidator.Validate(request);

        if (result.IsValid)
            return Ok(await _parcelaServices.GetListaParcelas(request.produto, request.condicaoPagamento));
        else
        {
            string response = "";
            result.Errors.ForEach(p => response += p.ErrorMessage + "\n");
            return BadRequest(response);
        }
    }
}