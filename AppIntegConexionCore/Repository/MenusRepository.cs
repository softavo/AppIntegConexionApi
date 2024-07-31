using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppIntegConexionCore.Repository
{
    public class MenusRepository : IMenusRepository, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly SqlConnection conexionDb;

        public MenusRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("ConnectionEmpresas");
            conexionDb = new SqlConnection(connectionString);
            if (conexionDb.State == 0)
            {
                conexionDb.Open();
            }
        }

        public List<MenuView> Consultar()
        {
            SqlCommand cmd = new SqlCommand("MenusQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<MenuView> listaMenus = new List<MenuView>();
            MenuView menu = null;

            while (dataReader.Read())
            {
                menu = new MenuView();

                menu.IdMenu = dataReader.ToInt("IdMenu");
                menu.Descripcion = dataReader.ToString("Descripcion");
                menu.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                menu.Url = dataReader.ToString("Url");
                menu.Orden = dataReader.ToInt("Orden");
                menu.MenuPadre = dataReader.ToString("MenuPadre");

                listaMenus.Add(menu);
            }

            return listaMenus;
        }
        public List<Menu> ConsultarPadres()
        {
            SqlCommand cmd = new SqlCommand("MenusPadresQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Menu> listaMenus = new List<Menu>();
            Menu menu = null;

            while (dataReader.Read())
            {
                menu = new Menu();

                menu.IdMenu = dataReader.ToInt("IdMenu");
                menu.Descripcion = dataReader.ToString("Descripcion");
                menu.Orden = dataReader.ToInt("Orden");

                listaMenus.Add(menu);
            }

            return listaMenus;
        }

        public List<Menu> ConsultarHijos(int id)
        {
            SqlCommand cmd = new SqlCommand("MenusHijosQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenuPadre", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Menu> listaMenus = new List<Menu>();
            Menu menu = null;

            while (dataReader.Read())
            {
                menu = new Menu();

                menu.IdMenu = dataReader.ToInt("IdMenu");
                menu.Descripcion = dataReader.ToString("Descripcion");
                menu.Orden = dataReader.ToInt("Orden");

                listaMenus.Add(menu);
            }

            return listaMenus;
        }

        public Menu ConsultarPorId(int? id)
        {
            SqlCommand cmd = new SqlCommand("MenusPorIdQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenu", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            Menu menu = null;

            if (dataReader.Read())
            {
                menu = new Menu();

                menu.IdMenu = dataReader.ToInt("IdMenu");
                menu.Descripcion = dataReader.ToString("Descripcion");
                menu.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                menu.Url = dataReader.ToString("Url");
                menu.Orden = dataReader.ToInt("Orden");
                //menu.MenuPadre = dataReader.ToString("MenuPadre");
                menu.VentanaNueva = dataReader.ToBool("VentanaNueva");
            }

            return menu;
        }

        public List<MenuView> ConsultarPorIdUsuario(int? id, bool completo)
        {
            SqlCommand cmd = new SqlCommand("MenusPorIdUsuarioQry", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            cmd.Parameters.AddWithValue("@Completo", completo);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<MenuView> objListaMenu = new List<MenuView>();

            List<MenuView> listaMenus = new List<MenuView>();
            List<MenuView> listaSubMenus = new List<MenuView>();

            MenuView objMenu = null;

            MenuView menu = null;
            MenuView submenu = null;

            while (dataReader.Read())
            {
                menu = new MenuView();
                submenu = new MenuView();

                if (dataReader.ToInt("IdMenuPadre") == 0)
                {
                    menu.IdMenu = dataReader.ToInt("IdMenu");
                    menu.Descripcion = dataReader.ToString("Descripcion");
                    menu.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                    menu.Url = dataReader.ToString("Url");
                    menu.Orden = dataReader.ToInt("Orden");
                    menu.VentanaNueva = dataReader.ToBool("VentanaNueva");
                    listaMenus.Add(menu);
                }
                else
                {
                    submenu.IdMenu = dataReader.ToInt("IdMenu");
                    submenu.Descripcion = dataReader.ToString("Descripcion");
                    submenu.IdMenuPadre = dataReader.ToInt("IdMenuPadre");
                    submenu.Url = dataReader.ToString("Url");
                    submenu.Orden = dataReader.ToInt("Orden");
                    submenu.VentanaNueva = dataReader.ToBool("VentanaNueva");
                    listaSubMenus.Add(submenu);
                }

            }

            for (int intMenu = 0; intMenu < listaMenus.Count; intMenu++)
            {
                objMenu = new MenuView();

                objMenu.IdMenu = listaMenus[intMenu].IdMenu;
                objMenu.Descripcion = listaMenus[intMenu].Descripcion;
                objMenu.IdMenuPadre = listaMenus[intMenu].IdMenuPadre;
                objMenu.Url = listaMenus[intMenu].Url;
                objMenu.VentanaNueva = listaMenus[intMenu].VentanaNueva;

                MenuView objSubMenu = null;
                List<MenuView> objListaSubMenu = new List<MenuView>();
                for (int intSubMenu = 0; intSubMenu < listaSubMenus.Count; intSubMenu++)
                {
                    objSubMenu = new MenuView();

                    if (listaSubMenus[intSubMenu].IdMenuPadre == listaMenus[intMenu].IdMenu)
                    {
                        objSubMenu.IdMenu = listaSubMenus[intSubMenu].IdMenu;
                        objSubMenu.Descripcion = listaSubMenus[intSubMenu].Descripcion;
                        objSubMenu.IdMenuPadre = listaSubMenus[intSubMenu].IdMenuPadre;
                        objSubMenu.Url = listaSubMenus[intSubMenu].Url;
                        objSubMenu.VentanaNueva = listaSubMenus[intSubMenu].VentanaNueva;

                        objListaSubMenu.Add(objSubMenu);
                    }
                }
                objMenu.InverseIdMenuPadreNavigation = objListaSubMenu;

                objListaMenu.Add(objMenu);
            }

            return objListaMenu;
        }
        public void Crear(Menu menu)
        {
            SqlCommand cmd = new SqlCommand("MenusIns", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Descripcion", menu.Descripcion);
            cmd.Parameters.AddWithValue("@IdMenuPadre", menu.IdMenuPadre);
            cmd.Parameters.AddWithValue("@Url", menu.Url);
            cmd.Parameters.AddWithValue("@Orden", menu.Orden);
            cmd.Parameters.AddWithValue("@VentanaNueva", menu.VentanaNueva);
            cmd.ExecuteNonQuery();
        }

        public void Editar(Menu menu)
        {
            SqlCommand cmd = new SqlCommand("MenusUpd", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenu", menu.IdMenu);
            cmd.Parameters.AddWithValue("@Descripcion", menu.Descripcion);
            cmd.Parameters.AddWithValue("@IdMenuPadre", menu.IdMenuPadre);
            cmd.Parameters.AddWithValue("@Url", menu.Url);
            cmd.Parameters.AddWithValue("@Orden", menu.Orden);
            cmd.Parameters.AddWithValue("@VentanaNueva", menu.VentanaNueva);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            SqlCommand cmd = new SqlCommand("MenusDel", conexionDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMenu", id);
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
