using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers;

public class SociosController : Controller
{
    private readonly ISocioService _socios;

    public SociosController(ISocioService socios) => _socios = socios;

    public async Task<IActionResult> Index(string? buscar)
    {
        ViewData["Buscar"] = buscar;
        return View(await _socios.ListarAsync(buscar));
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Documento,Nombres,Apellidos,Telefono,Correo,FechaNacimiento")] Socio socio)
    {
        if (await _socios.ExisteDocumentoAsync(socio.Documento))
            ModelState.AddModelError(nameof(socio.Documento), "Ya existe un socio con ese documento.");

        if (!ModelState.IsValid) return View(socio);

        await _socios.CrearAsync(socio);
        TempData["Ok"] = "Socio registrado.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var socio = await _socios.ObtenerAsync(id);
        return socio is null ? NotFound() : View(socio);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("IdSocio,Documento,Nombres,Apellidos,Telefono,Correo,FechaNacimiento")] Socio socio)
    {
        if (id != socio.IdSocio) return NotFound();

        if (await _socios.ExisteDocumentoAsync(socio.Documento, socio.IdSocio))
            ModelState.AddModelError(nameof(socio.Documento), "Ya existe un socio con ese documento.");

        if (!ModelState.IsValid) return View(socio);

        await _socios.ActualizarAsync(socio);
        TempData["Ok"] = "Socio actualizado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id)
    {
        await _socios.CambiarEstadoAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
