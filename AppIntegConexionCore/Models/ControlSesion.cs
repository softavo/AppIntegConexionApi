using System;

namespace AppIntegConexionCore.Models
{
    public partial class ControlSesion
    {
        public string Usuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaExpiracion { get; set; }
    }
}
