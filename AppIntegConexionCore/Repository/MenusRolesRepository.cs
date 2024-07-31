using AppIntegConexionCore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AppIntegConexionCore.Interfaces;

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

        public List<MenuRol> Consultar()
        {
            SqlCommand cmd = new SqlCommand("MenusRolesQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<MenuRol> listaMenusRol = new List<MenuRol>();
            MenuRol menusRol = null;

            while (dataReader.Read())
            {
                menusRol = new MenuRol();

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
                menusRol.MenuPadre = dataReader.ToString("MenuPadre");
                menusRol.MenuHijo = dataReader.ToString("MenuHijo");
                menusRol.Rol = dataReader.ToString("Rol");
            }

            return menusRol;
        }

        public async Task Crear(MenuRol menusRol)
        {
            SqlCommand cmd = new SqlCommand("MenusRolesIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdRol", menusRol.IdRol);
            cmd.Parameters.AddWithValue("@IdMenuPadre", menusRol.IdMenuPadre);
            cmd.Parameters.AddWithValue("@IdMenuHijo", menusRol.IdMenuHijo);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> Editar(MenuRol menusRol)
        {
            SqlCommand cmd = new SqlCommand("RolesMenusUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", menusRol.IdRol);
            cmd.Parameters.AddWithValue("@IdMenuPadre", menusRol.IdMenuPadre);
            cmd.Parameters.AddWithValue("@IdMenuHijo", menusRol.IdMenuHijo);

            int rows = await cmd.ExecuteNonQueryAsync();

            return (rows > 0);
        }

        public async Task<bool> Eliminar(int id)
        {
            SqlCommand cmd = new SqlCommand("MenusRolesDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenuRol", id);

            int rows = await cmd.ExecuteNonQueryAsync();

            return (rows > 0);
        }

        public List<MenuRol> ConsultarMenusRol(int idRol)
        {
            SqlCommand cmd = new SqlCommand("MenuRolesIdRolQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdRol", idRol);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<MenuRol> listaMenusRol = new List<MenuRol>();
            MenuRol menusRol = null;

            while (dataReader.Read())
            {
                menusRol = new MenuRol();

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
