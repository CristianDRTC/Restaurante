using Gym.Data;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services;

public class ClienteService : IClienteService
{
    private readonly RestauranteContext _db;

    public ClienteService(RestauranteContext db) => _db = db;

    public async Task<List<Cliente>> ListarAsync(string? buscar)
    {
        var q = _db.Clientes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(buscar))
            q = q.Where(cliente => cliente.Documento.Contains(buscar)
                          || cliente.Nombres.Contains(buscar)
                          || cliente.Apellidos.Contains(buscar));

        return await q.OrderBy(cliente => cliente.Apellidos).ThenBy(cliente => cliente.Nombres).ToListAsync();
    }

    public Task<Cliente?> ObtenerAsync(int id) =>
        _db.Clientes.AsNoTracking().FirstOrDefaultAsync(cliente => cliente.IdCliente == id);

    public Task<bool> ExisteDocumentoAsync(string documento, int? excluirId = null) =>
        _db.Clientes.AnyAsync(cliente => cliente.Documento == documento && cliente.IdCliente != excluirId);

    public async Task CrearAsync(Cliente cliente)
    {
        cliente.Activo = true;
        _db.Clientes.Add(cliente);
        await _db.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Cliente cliente)
    {
        var actual = await _db.Clientes.FindAsync(cliente.IdCliente);
        if (actual is null) return;

        actual.Documento = cliente.Documento;
        actual.Nombres = cliente.Nombres;
        actual.Apellidos = cliente.Apellidos;
        actual.Telefono = cliente.Telefono;
        actual.Correo = cliente.Correo;

        await _db.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente is null) return;

        cliente.Activo = !cliente.Activo;
        await _db.SaveChangesAsync();
    }
}
