using AppIntegConexionCore.Models;
using System.Collections.Generic;

namespace AppIntegConexionCore.Interfaces
{
    public interface IEmpresasRepository
    {
        List<Conexion> ConsultarConexionPorUsuario(string usuario);
        Conexion ConsultarConexionPorUsuarioEmpresa(string usuario, int conexion);
        void CrearEmpresa(Conexion empresa);
        void ActualizarEmpresa(Conexion empresa);
    }
}
    