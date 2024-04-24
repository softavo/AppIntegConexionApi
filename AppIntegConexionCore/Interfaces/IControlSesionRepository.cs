using AppIntegConexionCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppIntegConexionCore.Interfaces
{
    public interface IControlSesionRepository
    {
        void CrearSesion(ControlSesion controlSesion);
        ControlSesion ConsultarSesion(string usuario, int idConexion);
        void EliminarSesion(string usuario, int idConexion);
        void ActualizarSesion(ControlSesion controlSesion);
    }
}
