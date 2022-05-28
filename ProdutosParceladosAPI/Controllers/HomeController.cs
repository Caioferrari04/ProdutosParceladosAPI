using Microsoft.AspNetCore.Mvc;
using ProdutosParceladosAPI.Services;

namespace ProdutosParceladosAPI.Controllers;

public class HomeController : Controller 
{
    private readonly ParcelaServices _parcelaServices;
    public HomeController(ParcelaServices parcelaServices)
    {
        _parcelaServices = parcelaServices;
    }

    public IActionResult Index() 
    {
        return Ok();
    }
}