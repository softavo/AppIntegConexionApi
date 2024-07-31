using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppIntegConexionCore.Repository
{
    public class RolesRepository : IRolesRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public RolesRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public IList<Rol> Consultar()
        {
            SqlCommand cmd = new SqlCommand("RolesQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            IList<Rol> listaRoles = new List<Rol>();
            Rol rol = null;

            while (dataReader.Read())
            {
                rol = new Rol();

                rol.IdRol = dataReader.ToInt("IdRol");
                rol.Descripcion = dataReader.ToString("Descripcion");
                rol.SysAdmin = dataReader.ToBool("SysAdmin");
                rol.SysEmp = dataReader.ToBool("SysEmp");
                listaRoles.Add(rol);
            }
            return listaRoles;
        }

        public IList<Rol> ConsultarRolesUsuario(int idUsuario)
        {
            SqlCommand cmd = new SqlCommand("RolesIdUsuarioQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
            SqlDataReader dataReader = cmd.ExecuteReader();

            IList<Rol> listaRoles = new List<Rol>();
            Rol rol = null;

            while (dataReader.Read())
            {
                rol = new Rol();

                rol.IdRol = dataReader.ToInt("IdRol");
                rol.Descripcion = dataReader.ToString("Descripcion");
                listaRoles.Add(rol);
            }
            return listaRoles;
        }

        public Rol ConsultarPorId(int? id)
        {
            SqlCommand cmd = new SqlCommand("RolesPorIdQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            Rol rol = null;

            if (dataReader.Read())
            {
                rol = new Rol();

                rol.IdRol = dataReader.ToInt("IdRol");
                rol.Descripcion = dataReader.ToString("Descripcion");
                rol.SysAdmin = dataReader.ToBool("SysAdmin");
                rol.SysEmp = dataReader.ToBool("SysEmp");
            }
            return rol;
        }

        public IList<Rol> ConsultarRolesDisponibles(int? idUsuarioRol, int idUsuario)
        {
            SqlCommand cmd = new SqlCommand("RolesDisponiblesPorIdUsuarioQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuarioRol", idUsuarioRol);
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Rol> listaRolesDisponibles = new List<Rol>();
            Rol rolesDisponibles = null;

            while (dataReader.Read())
            {
                rolesDisponibles = new Rol();

                rolesDisponibles.IdRol = dataReader.ToInt("IdRol");
                rolesDisponibles.Descripcion = dataReader.ToString("Descripcion");

                listaRolesDisponibles.Add(rolesDisponibles);
            }

            return listaRolesDisponibles;
        }
        public void Crear(Rol rol)
        {
            SqlCommand cmd = new SqlCommand("RolesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
            cmd.Parameters.AddWithValue("@SysAdmin", rol.SysAdmin);
            cmd.Parameters.AddWithValue("@SysEmp", rol.SysEmp);
            cmd.ExecuteNonQuery();
        }

        public void Editar(Rol rol)
        {
            SqlCommand cmd = new SqlCommand("RolesUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
            cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
            cmd.Parameters.AddWithValue("@SysAdmin", rol.SysAdmin);
            cmd.Parameters.AddWithValue("@SysEmp", rol.SysEmp);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            SqlCommand cmd = new SqlCommand("RolesDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", id);
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
