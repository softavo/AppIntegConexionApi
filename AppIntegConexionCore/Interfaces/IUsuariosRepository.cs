using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IUsuariosRepository
    {
        public IList<Usuario> Consultar(int idUsuario, int idConexion);
        Usuario ConsultarUsuarioPorUsuarioClave(Usuario usuarioApi);
        void CrearUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(string codigo);
        void EliminarUsuarioEmpresa(string codigo, int idConexion);
    }
}
