using AppIntegConexionCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
