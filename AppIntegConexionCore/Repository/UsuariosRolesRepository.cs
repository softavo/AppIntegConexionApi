using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppIntegConexionCore.Repository
{
    public class UsuariosRolesRepository : IUsuariosRolesRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public UsuariosRolesRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public IList<UsuarioRol> Consultar()
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolesQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<UsuarioRol> listaUsuariosRol = new List<UsuarioRol>();
            UsuarioRol usuariosRol = null;

            while (dataReader.Read())
            {
                usuariosRol = new UsuarioRol();

                usuariosRol.IdUsuarioRol = dataReader.ToInt("IdUsuarioRol");
                usuariosRol.IdUsuario = dataReader.ToInt("IdUsuario");
                usuariosRol.IdRol = dataReader.ToInt("IdRol");
                //usuariosRol.Usuario = dataReader.ToString("Usuario");
                usuariosRol.Rol = dataReader.ToString("Rol");

                listaUsuariosRol.Add(usuariosRol);
            }
            return listaUsuariosRol;
        }

        public UsuarioRol ConsultarPorId(int? id)
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolesPorIdQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuarioRol", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            UsuarioRol usuariosRol = null;

            if (dataReader.Read())
            {
                usuariosRol = new UsuarioRol();

                usuariosRol.IdUsuarioRol = dataReader.ToInt("IdUsuarioRol");
                usuariosRol.IdUsuario = dataReader.ToInt("IdUsuario");
                usuariosRol.IdRol = dataReader.ToInt("IdRol");
                usuariosRol.Usuario = dataReader.ToString("Usuario");
                usuariosRol.Rol = dataReader.ToString("Rol");
            }
            return usuariosRol;
        }

        public IList<UsuarioRol> ConsultarPorIdUsuario(int? id)
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolesPorIdUsuarioQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<UsuarioRol> listaUsuariosRol = new List<UsuarioRol>();
            UsuarioRol usuariosRol = null;

            while (dataReader.Read())
            {
                usuariosRol = new UsuarioRol();

                usuariosRol.IdUsuarioRol = dataReader.ToInt("IdUsuarioRol");
                usuariosRol.IdUsuario = dataReader.ToInt("IdUsuario");
                usuariosRol.IdRol = dataReader.ToInt("IdRol");
                usuariosRol.Usuario = dataReader.ToString("Usuario");
                usuariosRol.Rol = dataReader.ToString("Rol");
                usuariosRol.SysAdmin = dataReader.ToBool("SysAdmin");
                usuariosRol.SysEmp = dataReader.ToBool("SysEmp");
                listaUsuariosRol.Add(usuariosRol);
            }
            return listaUsuariosRol;
        }

        public IList<UsuarioRol> Buscar(int idRol)
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolBuscarQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", idRol);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<UsuarioRol> listaUsuariosRol = new List<UsuarioRol>();
            UsuarioRol usuariosRol = null;

            while (dataReader.Read())
            {
                usuariosRol = new UsuarioRol();

                usuariosRol.IdUsuario = dataReader.ToInt("IdUsuario");
                usuariosRol.Nombre = dataReader.ToString("Nombre");
                listaUsuariosRol.Add(usuariosRol);
            }
            return listaUsuariosRol;
        }

        public void Crear(UsuarioRol usuarioRol)
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", usuarioRol.IdUsuario);
            cmd.Parameters.AddWithValue("@IdRol", usuarioRol.IdRol);
            cmd.ExecuteNonQuery();
        }

        public void Editar(UsuarioRol usuarioRol)
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolesUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", usuarioRol.IdUsuario);
            cmd.Parameters.AddWithValue("@IdRol", usuarioRol.IdRol);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            SqlCommand cmd = new SqlCommand("UsuariosRolesDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuarioRol", id);
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
