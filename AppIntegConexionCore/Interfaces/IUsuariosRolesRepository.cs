using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IUsuariosRolesRepository
    {
        IList<UsuarioRolView> ConsultarPorIdUsuario(int? id);
        UsuarioRol ConsultarPorId(int? id);
        IList<UsuarioRolView> Consultar();
        IList<UsuarioRolView> Buscar(int idRol);
        void Crear(UsuarioRol usuarioRol);
        void Editar(UsuarioRol usuarioRol);
        void Eliminar(int id);
    }
}
