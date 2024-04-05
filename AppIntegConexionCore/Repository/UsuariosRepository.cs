using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppIntegConexionCore.Repository
{
    public class UsuariosRepository :IUsuariosRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public UsuariosRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public Usuario ConsultarUsuarioPorUsuarioClave(Usuario usuarioApi)
        {
            SqlCommand cmd = new SqlCommand("UsuariosPorUsuarioClaveQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", usuarioApi.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuarioApi.Clave);
            SqlDataReader dataReader = cmd.ExecuteReader();

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
            SqlCommand cmd = new SqlCommand("UsuariosIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
            cmd.Parameters.AddWithValue("@IdConexion", usuario.IdConexion);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            SqlCommand cmd = new SqlCommand("UsuariosUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
            cmd.Parameters.AddWithValue("@IdConexion", usuario.IdConexion);
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuario(string usuario)
        {
            SqlCommand cmd = new SqlCommand("UsuariosDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuarioEmpresa(string codigo, int idConexion)
        {
            SqlCommand cmd = new SqlCommand("UsuariosConexionDel", conexionDb);
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
