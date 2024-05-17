using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppIntegConexionCore.Repository
{
    public class UsuariosRepository :IUsuariosRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly MySqlConnection conexionDb;

        public UsuariosRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new MySqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public Usuario ConsultarUsuarioPorUsuarioClave(Usuario usuarioApi)
        {
            MySqlCommand cmd = new MySqlCommand("UsuariosPorUsuarioClaveQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", usuarioApi.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuarioApi.Clave);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            Usuario usuario = null;

            if (dataReader.Read())
            {
                usuario = new Usuario();

                usuario.IdUsuario = dataReader.ToInt("IdUsuario");
                usuario.Codigo = dataReader.ToString("Codigo");
                usuario.Clave = dataReader.ToString("Clave");
            }
            return usuario;
        }

        public void CrearUsuario(Usuario usuario)
        {
            MySqlCommand cmd = new MySqlCommand("UsuariosIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
            cmd.Parameters.AddWithValue("@IdConexion", usuario.IdConexion);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            MySqlCommand cmd = new MySqlCommand("UsuariosUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
            cmd.Parameters.AddWithValue("@IdConexion", usuario.IdConexion);
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuario(string usuario)
        {
            MySqlCommand cmd = new MySqlCommand("UsuariosDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuarioEmpresa(string codigo, int idConexion)
        {
            MySqlCommand cmd = new MySqlCommand("UsuariosConexionDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", codigo);
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
