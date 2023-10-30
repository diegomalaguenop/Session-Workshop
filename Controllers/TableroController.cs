using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

public class TableroController : Controller
{

    public IActionResult Inicio()
    {
        return View();
    }

    public IActionResult Random()
    {
        var usuario = ObtenerUsuarioDeSesion();
        if (usuario != null)
        {
            Random rand = new Random();
            int valorAleatorio = rand.Next(1, 11);
            usuario.ValorNumerico += valorAleatorio;
            GuardarUsuarioEnSesion(usuario);
        }
        return RedirectToAction("Tablero");
    }


    [HttpPost]
    public IActionResult RealizarOperacion(string operacion)
    {
        var usuario = ObtenerUsuarioDeSesion();
        if (usuario != null)
        {

            if (operacion == "sumar")
            {
                usuario.ValorNumerico += 1;
            }
            else if (operacion == "restar")
            {
                usuario.ValorNumerico -= 1;
            }
            else if (operacion == "multiplicar")
            {
                usuario.ValorNumerico *= 2;
            }
            else if (operacion == "random")
            {
                Random rand = new Random();
                int valorAleatorio = rand.Next(1, 11);
                usuario.ValorNumerico += valorAleatorio;
            }

            GuardarUsuarioEnSesion(usuario);
        }
        return RedirectToAction("Tablero");
    }


    private Usuario ObtenerUsuarioDeSesion()
    {
        var usuarioJson = HttpContext.Session.GetString("Usuario");
        return usuarioJson != null ? JsonSerializer.Deserialize<Usuario>(usuarioJson) : null;
    }


    private void GuardarUsuarioEnSesion(Usuario usuario)
    {
        var usuarioJson = JsonSerializer.Serialize(usuario);
        HttpContext.Session.SetString("Usuario", usuarioJson);
    }

    public IActionResult Tablero()
{
    var usuario = ObtenerUsuarioDeSesion();
    if (usuario != null)
    {
        return View(usuario);
    }
    return RedirectToAction("Inicio");
}

[HttpPost]
public IActionResult InicioSesion(string Nombre)
{
    return RedirectToAction("Tablero", "Tablero");
}


}
