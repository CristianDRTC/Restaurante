using Gym.Data;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services;

public class SocioService : ISocioService
{
    private readonly GymContext _db;

    public SocioService(GymContext db) => _db = db;

    public async Task<List<Socio>> ListarAsync(string? buscar)
    {
        var q = _db.Socios.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(buscar))
            q = q.Where(s => s.Documento.Contains(buscar)
                          || s.Nombres.Contains(buscar)
                          || s.Apellidos.Contains(buscar));

        return await q.OrderBy(s => s.Apellidos).ThenBy(s => s.Nombres).ToListAsync();
    }

    public Task<Socio?> ObtenerAsync(int id) =>
        _db.Socios.AsNoTracking().FirstOrDefaultAsync(s => s.IdSocio == id);

    public Task<bool> ExisteDocumentoAsync(string documento, int? excluirId = null) =>
        _db.Socios.AnyAsync(s => s.Documento == documento && s.IdSocio != excluirId);

    public async Task CrearAsync(Socio socio)
    {
        socio.Activo = true;
        _db.Socios.Add(socio);
        await _db.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Socio socio)
    {
        var actual = await _db.Socios.FindAsync(socio.IdSocio);
        if (actual is null) return;

        actual.Documento = socio.Documento;
        actual.Nombres = socio.Nombres;
        actual.Apellidos = socio.Apellidos;
        actual.Telefono = socio.Telefono;
        actual.Correo = socio.Correo;
        actual.FechaNacimiento = socio.FechaNacimiento;

        await _db.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id)
    {
        var socio = await _db.Socios.FindAsync(id);
        if (socio is null) return;

        socio.Activo = !socio.Activo;
        await _db.SaveChangesAsync();
    }
}
