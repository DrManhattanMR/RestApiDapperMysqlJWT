using Dapper;
using Entidades;
using MySql.Data.MySqlClient;

namespace AccesoDatos;

public class ServiciosCatalogo
{
    public IEnumerable<Rol> ObtenerRoles()
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        IEnumerable<Rol> roles = new List<Rol>();
        try
        {
            var sql = "SELECT *FROM ROLES ";
            roles = conexion.Query<Rol>(sql).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }

        return roles;
    }

    public bool InserRol(Rol entidad)
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        bool _rowaffected = false;
        try
        {
            var sql = " insert into Roles (descripcion, active) values (@descripcion, @active); ";
            _rowaffected = conexion.Execute(sql, entidad) > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }
        return _rowaffected;
    }
}