using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IRolesRepository
    {
        IList<Rol> Consultar();
        IList<Rol> ConsultarRolesUsuario(int idUsuario);
        IList<Rol> ConsultarRolesDisponibles(int? idUsuarioRol, int idUsuario);
        Rol ConsultarPorId(int? id);
        Task Crear(Rol rol);
        Task<bool> Editar(Rol rol);
        Task<bool> Eliminar(int id);
    }
}
