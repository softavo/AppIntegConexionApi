﻿namespace AppIntegConexionCore.Models
{
    public partial class MenuRol
    {
        public int IdMenuRol { get; set; }
        public int IdRol { get; set; }
        public int IdMenuPadre { get; set; }
        public int IdMenuHijo { get; set; }
    }
}
