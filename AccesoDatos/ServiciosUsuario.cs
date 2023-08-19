using Entidades;
using Dapper;
using MySql.Data.MySqlClient;

namespace AccesoDatos;

public class ServiciosUsuario
{
    public Usuario Login(Usuario entidad)
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        Usuario user = new();
        try
        {
            ServiciosGeneral srvGeneral = new();
            string stringHash = srvGeneral.PasswordToSha256(entidad.password);
            var sql = " SELECT *FROM USUARIOS WHERE username=@username AND password=@password ";
            user = conexion.QuerySingle<Usuario>(sql, new { username = entidad.username, password = stringHash });
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }

        return user;
    }
    public IEnumerable<Usuario> ObtenerUsuarios()
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        IEnumerable<Usuario> _listausuario = new List<Usuario>();
        try
        {
            var sql = "SELECT *FROM USUARIOS ";
            _listausuario = conexion.Query<Usuario>(sql).ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }

        foreach (var item in _listausuario)
        {
            item.password = "*****";
        }
        return _listausuario;
    }

    public bool InsertUsuario(Usuario entidad)
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        bool result = false;
        try
        {
            ServiciosGeneral srvgeneral = new();
            entidad.password = srvgeneral.GenerarPasswordDefaul(entidad.nombre,entidad.telefono);
            var sql =
                "insert into Usuarios ( nombre, apellidos, username, direccion, correo, telefono, password, active, role) " +
                "values (@nombre, @apellidos, @username, @direccion, @correo, @telefono, @password, @active, @role)";
            result = conexion.Execute(sql, entidad) > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }

        return result;
    }

    public bool UpdateUser(Usuario entidad)
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        bool result = false;
        try
        {
            var sql =
                " update usuarios set nombre=@nombre, apellidos=@apellidos, username=@username, " +
                "direccion=@direccion, correo=@correo, telefono=@telefono, active=@active, role=@role " +
                "where id=@id ";
            result = conexion.Execute(sql, entidad) > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }

        return result;
    }

    public bool DeleteUser(int id)
    {
        MySqlConnection conexion = ServiciosBD.ObtenerConexion();
        bool result = false;
        try
        {
            var sql = "delete from usuarios where id = @id ";
            result = conexion.Execute(sql, new { id = id }) > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conexion.Close();
            conexion.Dispose();
            MySqlConnection.ClearPool(conexion);
        }

        return result;
    }
}