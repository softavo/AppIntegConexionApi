using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IUsuariosRepository
    {
        public IList<Usuario> Consultar(int idUsuario, int idConexion);
        Usuario ConsultarUsuarioPorUsuarioClave(Usuario usuarioApi);
        int CrearUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(string codigo);
        //void EliminarUsuarioEmpresa(string codigo, int idConexion);


        IList<Usuario> ConsultarIdUsuario(int idUsuario);
        IList<Usuario> Buscar();
        Usuario ConsultarPorId(int? id);
        Usuario ConsultarPorCodigo(string codigo);
        Usuario ConsultarAprobacion(string codigo, string clave);
    }
}
