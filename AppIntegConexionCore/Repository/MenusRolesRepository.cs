using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppIntegConexionCore.Repository
{
    public class MenusRolesRepository : IMenusRolesRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public MenusRolesRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public List<MenuRolView> Consultar()
        {
            SqlCommand cmd = new SqlCommand("MenusRolesQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<MenuRolView> listaMenusRol = new List<MenuRolView>();
            MenuRolView menusRol = null;

            while (dataReader.Read())
            {
                menusRol = new MenuRolView();

                menusRol.IdMenuRol = dataReader.ToInt("IdMenuRol");
                menusRol.IdRol = dataReader.ToInt("IdRol");
                menusRol.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                menusRol.IdMenuHijo = dataReader.ToInt("IdMenuHijo");
                menusRol.MenuPadre = dataReader.ToString("MenuPadre");
                menusRol.MenuHijo = dataReader.ToString("MenuHijo");
                menusRol.Rol = dataReader.ToString("Rol");
                listaMenusRol.Add(menusRol);
            }
            return listaMenusRol;
        }

        public MenuRol ConsultarPorId(int? id)
        {
            SqlCommand cmd = new SqlCommand("MenusRolesPorIdQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenuRol", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            MenuRol menusRol = null;

            if (dataReader.Read())
            {
                menusRol = new MenuRol();

                menusRol.IdMenuRol = dataReader.ToInt("IdMenuRol");
                menusRol.IdRol = dataReader.ToInt("IdRol");
                menusRol.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                menusRol.IdMenuHijo = dataReader.ToInt("IdMenuHijo");
                //menusRol.MenuPadre = dataReader.ToString("MenuPadre");
                //menusRol.MenuHijo = dataReader.ToString("MenuHijo");
                //menusRol.Rol = dataReader.ToString("Rol");
            }
            return menusRol;
        }

        public MenuRolView ConsultarViewPorId(int id)
        {
            SqlCommand cmd = new SqlCommand("MenusRolesPorIdQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenuRol", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            MenuRolView menusRol = null;

            if (dataReader.Read())
            {
                menusRol = new MenuRolView();

                menusRol.IdMenuRol = dataReader.ToInt("IdMenuRol");
                menusRol.IdRol = dataReader.ToInt("IdRol");
                menusRol.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                menusRol.IdMenuHijo = dataReader.ToInt("IdMenuHijo");
                menusRol.MenuPadre = dataReader.ToString("MenuPadre");
                menusRol.MenuHijo = dataReader.ToString("MenuHijo");
                menusRol.Rol = dataReader.ToString("Rol");
            }
            return menusRol;
        }

        public void Crear(MenuRol menusRol)
        {
            SqlCommand cmd = new SqlCommand("MenusRolesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", menusRol.IdRol);
            cmd.Parameters.AddWithValue("@IdMenuPadre", menusRol.IdMenuPadre);
            cmd.Parameters.AddWithValue("@IdMenuHijo", menusRol.IdMenuHijo);
            cmd.ExecuteNonQuery();
        }

        public void Editar(MenuRol menusRol)
        {
            SqlCommand cmd = new SqlCommand("RolesMenusUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", menusRol.IdRol);
            cmd.Parameters.AddWithValue("@IdMenuPadre", menusRol.IdMenuPadre);
            cmd.Parameters.AddWithValue("@IdMenuHijo", menusRol.IdMenuHijo);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            SqlCommand cmd = new SqlCommand("MenusRolesDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenuRol", id);
            cmd.ExecuteNonQuery();
        }

        public List<MenuRolView> ConsultarMenusRol(int idRol)
        {
            SqlCommand cmd = new SqlCommand("MenuRolesIdRolQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", idRol);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<MenuRolView> listaMenusRol = new List<MenuRolView>();
            MenuRolView menusRol = null;

            while (dataReader.Read())
            {
                menusRol = new MenuRolView();

                menusRol.IdRol = dataReader.ToInt("IdRol");
                menusRol.IdMenuHijo = dataReader.ToInt("IdMenu");
                menusRol.MenuHijo = dataReader.ToString("Menu");

                listaMenusRol.Add(menusRol);
            }
            return listaMenusRol;
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
