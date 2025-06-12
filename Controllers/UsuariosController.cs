using Microsoft.AspNetCore.Mvc;

[Route("usuarios")]
public class UsuariosController : Controller
{
public IActionResult Index()
{
    return View();
}
}