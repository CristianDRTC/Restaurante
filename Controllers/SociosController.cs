using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers;

[Route("Clientes/[action]")]
public class SociosController : Controller
{
    private readonly IClienteService _clientes;

    public SociosController(IClienteService clientes) => _clientes = clientes;

    public async Task<IActionResult> Index(string? buscar)
    {
        ViewData["Buscar"] = buscar;
        return View(await _clientes.ListarAsync(buscar));
    }

    public IActionResult Create() => View(new Cliente());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Documento,Nombres,Apellidos,Telefono,Correo")] Cliente cliente)
    {
        if (await _clientes.ExisteDocumentoAsync(cliente.Documento))
            ModelState.AddModelError(nameof(cliente.Documento), "Ya existe un cliente con ese documento.");

        if (!ModelState.IsValid) return View(cliente);

        await _clientes.CrearAsync(cliente);
        TempData["Ok"] = "Cliente registrado.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _clientes.ObtenerAsync(id);
        return cliente is null ? NotFound() : View(cliente);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("IdCliente,Documento,Nombres,Apellidos,Telefono,Correo")] Cliente cliente)
    {
        if (id != cliente.IdCliente) return NotFound();

        if (await _clientes.ExisteDocumentoAsync(cliente.Documento, cliente.IdCliente))
            ModelState.AddModelError(nameof(cliente.Documento), "Ya existe un cliente con ese documento.");

        if (!ModelState.IsValid) return View(cliente);

        await _clientes.ActualizarAsync(cliente);
        TempData["Ok"] = "Cliente actualizado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id)
    {
        await _clientes.CambiarEstadoAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
