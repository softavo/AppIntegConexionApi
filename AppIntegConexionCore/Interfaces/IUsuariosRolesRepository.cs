using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IUsuariosRolesRepository
    {
        IList<UsuarioRol> ConsultarPorIdUsuario(int? id);
        UsuarioRol ConsultarPorId(int? id);
        IList<UsuarioRol> Consultar();
        IList<UsuarioRol> Buscar(int idRol);
        Task Crear(UsuarioRol usuarioRol);
        Task<bool> Editar(UsuarioRol usuarioRol);
        Task<int> Eliminar(int id);
    }
}
