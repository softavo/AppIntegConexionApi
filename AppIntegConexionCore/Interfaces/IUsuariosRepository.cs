using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IUsuariosRepository
    {
        Usuario ConsultarUsuarioPorUsuarioClave(Usuario usuarioApi);
        void CrearUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(string codigo);
        void EliminarUsuarioEmpresa(string codigo, int idConexion);
    }
}
