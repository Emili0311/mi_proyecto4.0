using Microsoft.AspNetCore.Mvc;

[Route("usuarios")]
public class UsuariosController : Controller
{
public IActionResult Index()
{
    return View();
}

   [Route("editar/{id?}")]
    public IActionResult Editar(string? id)
    {
        ViewBag.ID = id;
        return View();
    }
}