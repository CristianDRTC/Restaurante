using Gym.Models;

namespace Gym.Services;

public interface IClienteService
{
    Task<List<Cliente>> ListarAsync(string? buscar);
    Task<Cliente?> ObtenerAsync(int id);
    Task<bool> ExisteDocumentoAsync(string documento, int? excluirId = null);
    Task CrearAsync(Cliente cliente);
    Task ActualizarAsync(Cliente cliente);
    Task CambiarEstadoAsync(int id);
}
