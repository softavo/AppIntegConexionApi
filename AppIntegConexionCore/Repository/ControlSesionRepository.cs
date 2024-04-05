using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AppIntegConexionCore.Repository
{
    public class ControlSesionRepository : IControlSesionRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public ControlSesionRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }
        public void ActualizarSesion(ControlSesion controlSesion)
        {
            SqlCommand cmd = new SqlCommand("ControlSesionesUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", controlSesion.Usuario);
            cmd.Parameters.AddWithValue("@FechaExpiracion", controlSesion.FechaExpiracion);
            cmd.ExecuteNonQuery();
        }


        public ControlSesion ConsultarSesion(string usuario)
        {
            SqlCommand cmd = new SqlCommand("ControlSesionesQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            SqlDataReader dataReader = cmd.ExecuteReader();

            ControlSesion sesion = null;

            if (dataReader.Read())
            {
                sesion = new ControlSesion();

                sesion.Usuario = dataReader.ToString("Usuario");
                sesion.FechaInicio = dataReader.ToDateTime("FechaInicio");
                sesion.FechaExpiracion = dataReader.ToNullableDateTime("FechaExpiracion");
            }
            return sesion;
        }

        public void CrearSesion(ControlSesion controlSesion)
        {
            SqlCommand cmd = new SqlCommand("ControlSesionesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", controlSesion.Usuario);
            cmd.Parameters.AddWithValue("@FechaInicio", controlSesion.FechaInicio);
            cmd.ExecuteNonQuery();
        }

        public void EliminarSesion(string usuario)
        {
            SqlCommand cmd = new SqlCommand("ControlSesionesDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);

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
