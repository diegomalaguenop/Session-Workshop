using Microsoft.AspNetCore.Mvc;

public class AutenticacionController : Controller
{

    public IActionResult InicioSesion()
    {
        return View();
    }

    [HttpPost]
    public IActionResult InicioSesion(Usuario usuario)
    {
        return Content($"¡Bienvenido, {usuario.Nombre}!");
    }
}
