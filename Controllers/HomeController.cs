using Microsoft.AspNetCore.Mvc;

namespace ProdutosParceladosAPI.Controllers;

public class HomeController : Controller 
{
    public async Task<IActionResult> Index() 
    {
        return Ok();
    }
}