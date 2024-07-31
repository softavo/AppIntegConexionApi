using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IRolesRepository
    {
        IList<Rol> Consultar();
        IList<Rol> ConsultarRolesUsuario(int idUsuario);
        IList<Rol> ConsultarRolesDisponibles(int? idUsuarioRol, int idUsuario);
        Rol ConsultarPorId(int? id);
        void Crear(Rol rol);
        void Editar(Rol rol);
        void Eliminar(int id);
    }
}
