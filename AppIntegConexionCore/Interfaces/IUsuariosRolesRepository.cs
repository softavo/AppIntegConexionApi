using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IUsuariosRolesRepository
    {
        IList<UsuarioRol> ConsultarPorIdUsuario(int? id);
        UsuarioRol ConsultarPorId(int? id);
        IList<UsuarioRol> Consultar();
        IList<UsuarioRol> Buscar(int idRol);
        void Crear(UsuarioRol usuarioRol);
        void Editar(UsuarioRol usuarioRol);
        void Eliminar(int id);
    }
}
