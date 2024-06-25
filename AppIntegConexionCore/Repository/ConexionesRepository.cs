using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppIntegConexionCore.Repository
{
    public class ConexionesRepository : IConexionesRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public ConexionesRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public void ActualizarEmpresa(Conexion empresa)
        {
            SqlCommand cmd = new SqlCommand("ConexionesUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdConexion", empresa.IdConexion);
            cmd.Parameters.AddWithValue("@NombreCompania", empresa.NombreCompania);
            cmd.Parameters.AddWithValue("@StringConnection", empresa.StringConnection);
            cmd.Parameters.AddWithValue("@ServerName", empresa.ServerName);
            cmd.Parameters.AddWithValue("@DbName", empresa.DbName);
            cmd.Parameters.AddWithValue("@UserName", empresa.UserName);
            cmd.Parameters.AddWithValue("@Password", empresa.Password);
            cmd.Parameters.AddWithValue("@Nit", empresa.Nit);
            cmd.Parameters.AddWithValue("@DV", empresa.DV);
            cmd.Parameters.AddWithValue("@RepLegal", empresa.RepLegal);
            cmd.Parameters.AddWithValue("@Telefono", empresa.Telefono);
            cmd.Parameters.AddWithValue("@Correo", empresa.Correo);
            cmd.Parameters.AddWithValue("@Pais", empresa.Pais);
            cmd.Parameters.AddWithValue("@Departamento", empresa.Departamento);
            cmd.Parameters.AddWithValue("@Municipio", empresa.Municipio);
            cmd.Parameters.AddWithValue("@Barrio", empresa.Barrio);
            cmd.Parameters.AddWithValue("@Direccion", empresa.Direccion);
            cmd.Parameters.AddWithValue("@TextoResolucion", empresa.TextoResolucion);
            cmd.Parameters.AddWithValue("@Resolucion", empresa.Resolucion);
            cmd.Parameters.AddWithValue("@IvaIncluido", empresa.IvaIncluido);
            cmd.Parameters.AddWithValue("@FacturaSinInventario", empresa.FacturaSinInventario);
            cmd.Parameters.AddWithValue("@ImprimirPos", empresa.ImprimirPos);
            cmd.Parameters.AddWithValue("@Completo", empresa.Completo);
            cmd.Parameters.AddWithValue("@MinutosExpiraSesion", empresa.MinutosExpiraSesion);
            cmd.Parameters.AddWithValue("@CantidadUsuarios", empresa.CantidadUsuarios);
            cmd.Parameters.AddWithValue("@ModificaPrecio", empresa.ModificaPrecio);
            cmd.Parameters.AddWithValue("@DiferenciaHoraria", empresa.DiferenciaHoraria);
            cmd.Parameters.AddWithValue("@MensajeGeneral", empresa.MensajeGeneral);
            cmd.Parameters.AddWithValue("@ImprimirAgrupado", empresa.ImprimirAgrupado);
            cmd.Parameters.AddWithValue("@EnviaFacturaCorreo", empresa.EnviaFacturaCorreo);
            cmd.ExecuteNonQuery();
        }

        public List<Conexion> ConsultarConexionPorUsuario(string codigo)
        {
            SqlCommand cmd = new SqlCommand("ConexionesPorCodigoQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            SqlDataReader dataReader = cmd.ExecuteReader();
            List<Conexion> listaEmpresas = new List<Conexion>();
            Conexion conexion = null;

            while (dataReader.Read())
            {
                conexion = new Conexion();

                conexion.IdConexion = dataReader.ToInt("IdConexion");
                conexion.NombreCompania = dataReader.ToString("NombreCompania");
                conexion.StringConnection = dataReader.ToString("StringConnection");
                listaEmpresas.Add(conexion);
            }
            return listaEmpresas;
        }

        public Conexion ConsultarConexionPorUsuarioEmpresa(string codigo, int idConexion)
        {
            SqlCommand cmd = new SqlCommand("ConexionesPorCodigoEmpresaQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            cmd.Parameters.AddWithValue("@IdConexion", idConexion);
            SqlDataReader dataReader = cmd.ExecuteReader();
            Conexion conexion = null;

            if (dataReader.Read())
            {
                conexion = new Conexion();

                conexion.IdConexion = dataReader.ToInt("IdConexion");
                conexion.NombreCompania = dataReader.ToString("NombreCompania");
                conexion.StringConnection = dataReader.ToString("StringConnection");
                conexion.ServerName = dataReader.ToString("ServerName");
                conexion.DbName = dataReader.ToString("DbName");
                conexion.UserName = dataReader.ToString("UserName");
                conexion.Password = dataReader.ToString("Password");
                conexion.Nit = dataReader.ToString("Nit");
                conexion.DV = dataReader.ToInt("DV");
                conexion.Nit = dataReader.ToString("Nit");
                conexion.RepLegal = dataReader.ToString("RepLegal");
                conexion.Telefono = dataReader.ToString("Telefono");
                conexion.Correo = dataReader.ToString("Correo");
                conexion.Pais = dataReader.ToString("Pais");
                conexion.Departamento = dataReader.ToString("Departamento");
                conexion.Municipio = dataReader.ToString("Municipio");
                conexion.Direccion = dataReader.ToString("Direccion");
                conexion.Barrio = dataReader.ToString("Barrio");
                conexion.TextoResolucion = dataReader.ToString("TextoResolucion");
                conexion.Resolucion = dataReader.ToString("Resolucion");
                conexion.IvaIncluido = dataReader.ToBool("IvaIncluido");
                conexion.FacturaSinInventario = dataReader.ToBool("FacturaSinInventario");
                conexion.ImprimirPos = dataReader.ToBool("ImprimirPos");
                conexion.Completo = dataReader.ToBool("Completo");
                conexion.MinutosExpiraSesion = dataReader.ToInt("MinutosExpiraSesion");
                conexion.CantidadUsuarios = dataReader.ToInt("CantidadUsuarios");
                conexion.ModificaPrecio = dataReader.ToBool("ModificaPrecio");
                conexion.DiferenciaHoraria = dataReader.ToInt("DiferenciaHoraria");
                conexion.MensajeGeneral = dataReader.ToString("MensajeGeneral");
                conexion.ImprimirAgrupado = dataReader.ToBool("ImprimirAgrupado");
                conexion.EnviaFacturaCorreo = dataReader.ToBool("EnviaFacturaCorreo");
            }

            return conexion;
        }

        public void CrearEmpresa(Conexion empresa)
        {
            SqlCommand cmd = new SqlCommand("ConexionesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreCompania", empresa.NombreCompania);
            cmd.Parameters.AddWithValue("@StringConnection", empresa.StringConnection);
            cmd.Parameters.AddWithValue("@ServerName", empresa.ServerName);
            cmd.Parameters.AddWithValue("@DbName", empresa.DbName);
            cmd.Parameters.AddWithValue("@UserName", empresa.UserName);
            cmd.Parameters.AddWithValue("@Password", empresa.Password);
            cmd.Parameters.AddWithValue("@Nit", empresa.Nit);
            cmd.Parameters.AddWithValue("@DV", empresa.DV);
            cmd.Parameters.AddWithValue("@RepLegal", empresa.RepLegal);
            cmd.Parameters.AddWithValue("@Telefono", empresa.Telefono);
            cmd.Parameters.AddWithValue("@Correo", empresa.Correo);
            cmd.Parameters.AddWithValue("@Pais", empresa.Pais);
            cmd.Parameters.AddWithValue("@Departamento", empresa.Departamento);
            cmd.Parameters.AddWithValue("@Municipio", empresa.Municipio);
            cmd.Parameters.AddWithValue("@Barrio", empresa.Barrio);
            cmd.Parameters.AddWithValue("@Direccion", empresa.Direccion);
            cmd.Parameters.AddWithValue("@TextoResolucion", empresa.TextoResolucion);
            cmd.Parameters.AddWithValue("@Resolucion", empresa.Resolucion);
            cmd.Parameters.AddWithValue("@IvaIncluido", empresa.IvaIncluido);
            cmd.Parameters.AddWithValue("@FacturaSinInventario", empresa.FacturaSinInventario);
            cmd.Parameters.AddWithValue("@ImprimirPos", empresa.ImprimirPos);
            cmd.Parameters.AddWithValue("@Completo", empresa.Completo);
            cmd.Parameters.AddWithValue("@MinutosExpiraSesion", empresa.MinutosExpiraSesion);
            cmd.Parameters.AddWithValue("@CantidadUsuarios", empresa.CantidadUsuarios);
            cmd.Parameters.AddWithValue("@ModificaPrecio", empresa.ModificaPrecio);
            cmd.Parameters.AddWithValue("@DiferenciaHoraria", empresa.DiferenciaHoraria);
            cmd.Parameters.AddWithValue("@MensajeGeneral", empresa.MensajeGeneral);
            cmd.Parameters.AddWithValue("@ImprimirAgrupado", empresa.ImprimirAgrupado);
            cmd.Parameters.AddWithValue("@EnviaFacturaCorreo", empresa.EnviaFacturaCorreo);
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                conexionDb.Close();
            }
        }
    }
}
