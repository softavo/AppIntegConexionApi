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

        public IList<Usuario> Consultar(int idUsuario, int idConexion)
        {
            SqlCommand cmd = new SqlCommand("UsuariosPorIdConexionQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@IdConexion", idConexion);
            SqlDataReader dataReader = cmd.ExecuteReader();

            IList<Usuario> listaUsuarios = new List<Usuario>();
            Usuario usuario = null;

            while (dataReader.Read())
            {
                usuario = new Usuario();
                usuario.IdUsuario = dataReader.ToInt("IdUsuario");
                usuario.Codigo = dataReader.ToString("Codigo");
                usuario.Clave = dataReader.ToString("Clave");
                usuario.Nombre = dataReader.ToString("Nombre");
                usuario.Cargo = dataReader.ToString("Cargo");
                usuario.IdVendedor = dataReader.ToInt("IdVendedor");
                listaUsuarios.Add(usuario);
            }
            return listaUsuarios;
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

        public int CrearUsuario(Usuario usuario)
        {
            SqlCommand cmd = new SqlCommand("UsuariosIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Codigo", usuario.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave.ToUpper());
            cmd.Parameters.AddWithValue("@Cargo", usuario.Cargo);
            cmd.Parameters.AddWithValue("@IdVendedor", usuario.IdVendedor);
            SqlDataReader dataReader = cmd.ExecuteReader();
            int id = 0;

            if (dataReader.Read())
            {
                id = dataReader.ToInt("InsertedIDUSuario");
            }
            return id;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            SqlCommand cmd = new SqlCommand("UsuariosUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Codigo", usuario.Codigo);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave.ToUpper());
            cmd.Parameters.AddWithValue("@Cargo", usuario.Cargo);
            cmd.Parameters.AddWithValue("@IdVendedor", usuario.IdVendedor);
            cmd.ExecuteNonQueryAsync();
        }

        public void EliminarUsuario(string usuario)
        {
            SqlCommand cmd = new SqlCommand("UsuariosDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            cmd.ExecuteNonQuery();
        }

        //public void EliminarUsuarioEmpresa(string codigo, int idConexion)
        //{
        //    SqlCommand cmd = new SqlCommand("UsuariosConexionDel", conexionDb);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Usuario", codigo);
        //    cmd.Parameters.AddWithValue("@IdConexion", idConexion);
        //    cmd.ExecuteNonQuery();
        //}

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

        public IList<Usuario> ConsultarIdUsuario(int idUsuario)
        {
            SqlCommand cmd = new SqlCommand("UsuariosIdUsuarioQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
            SqlDataReader dataReader = cmd.ExecuteReader();
            IList<Usuario> listaUsuarios = new List<Usuario>();
            Usuario usuario = null;

            while (dataReader.Read())
            {
                usuario = new Usuario();
                usuario.IdUsuario = dataReader.ToInt("IdUsuario");
                usuario.Codigo = dataReader.ToString("Codigo");
                usuario.Clave = dataReader.ToString("Clave");
                usuario.Nombre = dataReader.ToString("Nombre");
                usuario.Cargo = dataReader.ToString("Cargo");
                usuario.IdVendedor = dataReader.ToInt("IdVendedor");
                listaUsuarios.Add(usuario);
            }
            return listaUsuarios;
        }

        public IList<Usuario> Buscar()
        {
            SqlCommand cmd = new SqlCommand("UsuariosBuscarQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();
            IList<Usuario> listaUsuarios = new List<Usuario>();
            Usuario usuario = null;

            while (dataReader.Read())
            {
                usuario = new Usuario();
                usuario.IdUsuario = dataReader.ToInt("IdUsuario");
                usuario.Nombre = dataReader.ToString("Nombre");
                listaUsuarios.Add(usuario);
            }
            return listaUsuarios;
        }

        public Usuario ConsultarPorId(int? id)
        {
            SqlCommand cmd = new SqlCommand("UsuariosPorIdQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            SqlDataReader dataReader = cmd.ExecuteReader();
            Usuario usuario = null;

            if (dataReader.Read())
            {
                usuario = new Usuario();
                usuario.IdUsuario = dataReader.ToInt("IdUsuario");
                usuario.Codigo = dataReader.ToString("Codigo");
                usuario.Clave = dataReader.ToString("Clave");
                usuario.Nombre = dataReader.ToString("Nombre");
                usuario.Cargo = dataReader.ToString("Cargo");
                usuario.IdVendedor = dataReader.ToInt("IdVendedor");
            }
            return usuario;
        }

        public Usuario ConsultarPorCodigo(string codigo)
        {
            SqlCommand cmd = new SqlCommand("UsuariosPorCodigoQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            SqlDataReader dataReader = cmd.ExecuteReader();

            Usuario usuario = null;

            if (dataReader.Read())
            {
                usuario = new Usuario();
                usuario.IdUsuario = dataReader.ToInt("IdUsuario");
                usuario.Codigo = dataReader.ToString("Codigo");
                usuario.Clave = dataReader.ToString("Clave");
                usuario.Nombre = dataReader.ToString("Nombre");
                usuario.Cargo = dataReader.ToString("Cargo");
                usuario.IdVendedor = dataReader.ToInt("IdVendedor");
            }
            return usuario;
        }

        public Usuario ConsultarAprobacion(string codigo, string clave)
        {
            SqlCommand cmd = new SqlCommand("UsuariosAprobarQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", codigo);
            cmd.Parameters.AddWithValue("@Clave", clave);
            SqlDataReader dataReader = cmd.ExecuteReader();
            Usuario usuario = null;

            if (dataReader.Read())
            {
                usuario = new Usuario();
                usuario.Codigo = dataReader.ToString("Codigo");
                usuario.Nombre = dataReader.ToString("Nombre");
            }
            return usuario;
        }
    }
}
