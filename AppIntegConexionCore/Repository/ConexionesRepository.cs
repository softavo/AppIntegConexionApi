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
                //conexion.IdUsuario = dataReader.ToInt("IdUsuario");
                //conexion.Codigo = dataReader.ToString("Codigo");
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
                //conexion.IdUsuario = dataReader.ToInt("IdUsuario");
                //conexion.Codigo = dataReader.ToString("Codigo");
            }

            return conexion;
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
