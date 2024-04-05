using AppIntegConexionCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppIntegConexionCore.Interfaces
{
    public interface IControlSesionRepository
    {
        void CrearSesion(ControlSesion controlSesion);
        ControlSesion ConsultarSesion(string usuario);
        void EliminarSesion(string usuario);
        void ActualizarSesion(ControlSesion controlSesion);
    }
}
