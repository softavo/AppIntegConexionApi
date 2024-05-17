using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppIntegConexionCore.Repository
{
    public class ControlSesionRepository : IControlSesionRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly MySqlConnection conexionDb;

        public ControlSesionRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new MySqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }
        public void ActualizarSesion(ControlSesion controlSesion)
        {
            MySqlCommand cmd = new MySqlCommand("ControlSesionesUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", controlSesion.Usuario);
            cmd.Parameters.AddWithValue("@FechaExpiracion", controlSesion.FechaExpiracion);
            cmd.Parameters.AddWithValue("@IdConexion", controlSesion.IdConexion);
            cmd.ExecuteNonQuery();
        }


        public ControlSesion ConsultarSesion(string usuario, int idConexion)
        {
            MySqlCommand cmd = new MySqlCommand("ControlSesionesQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            cmd.Parameters.AddWithValue("@IdConexion", idConexion);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            ControlSesion sesion = null;

            if (dataReader.Read())
            {
                sesion = new ControlSesion();

                sesion.Usuario = dataReader.ToString("Usuario");
                sesion.FechaInicio = dataReader.ToDateTime("FechaInicio");
                sesion.FechaExpiracion = dataReader.ToNullableDateTime("FechaExpiracion");
                sesion.Diferencia = dataReader.ToInt("Diferencia");
                sesion.IdConexion = dataReader.ToInt("IdConexion");
            }
            return sesion;
        }

        public void CrearSesion(ControlSesion controlSesion)
        {
            MySqlCommand cmd = new MySqlCommand("ControlSesionesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", controlSesion.Usuario);
            cmd.Parameters.AddWithValue("@FechaInicio", controlSesion.FechaInicio);
            cmd.Parameters.AddWithValue("@IdConexion", controlSesion.IdConexion);
            cmd.ExecuteNonQuery();
        }

        public void EliminarSesion(string usuario, int idConexion)
        {
            MySqlCommand cmd = new MySqlCommand("ControlSesionesDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            cmd.Parameters.AddWithValue("@IdConexion", idConexion);

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
