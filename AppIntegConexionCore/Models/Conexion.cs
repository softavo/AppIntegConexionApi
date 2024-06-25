namespace AppIntegConexionCore.Models
{
    public partial class Conexion
    {
        public int IdConexion { get; set; }
        public string StringConnection { get; set;}
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Nit { get; set; }
        public int DV { get; set; }
        public string NombreCompania { get; set; }
        public string RepLegal { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public string TextoResolucion { get; set; }
        public string Resolucion { get; set; }
        public bool IvaIncluido { get; set; }
        public bool FacturaSinInventario { get; set; }
        public bool ImprimirPos { get; set; }
        public bool Completo { get; set; }
        public int MinutosExpiraSesion { get; set; }
        public int CantidadUsuarios { get; set; }
        public bool ModificaPrecio { get; set; }
        public int DiferenciaHoraria { get; set; }
        public string MensajeGeneral { get; set; }
        public bool ImprimirAgrupado { get; set; }
        public bool EnviaFacturaCorreo { get; set; }
    }
}
