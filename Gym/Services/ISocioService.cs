using Gym.Models;

namespace Gym.Services;

public interface ISocioService
{
    Task<List<Socio>> ListarAsync(string? buscar);
    Task<Socio?> ObtenerAsync(int id);
    Task<bool> ExisteDocumentoAsync(string documento, int? excluirId = null);
    Task CrearAsync(Socio socio);
    Task ActualizarAsync(Socio socio);
    Task CambiarEstadoAsync(int id);
}
