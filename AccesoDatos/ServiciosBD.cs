using System.Data;
namespace AccesoDatos;
using MySql.Data.MySqlClient;
public class ServiciosBD
{
    public static MySqlConnection ObtenerConexion()
    {
        string username = "";
        string datasource = "";
        string port = "";
        string password = "";
        string database = "";
        MySqlConnection conexion = new MySqlConnection();
        string cadena = $"datasource={datasource};port={port};username={username};password={password};database={database};";
        conexion.ConnectionString = cadena;
        try
        {
            conexion.Open();
            return conexion;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static MySqlCommand ObtenerComando(MySqlConnection conn, string sql)
    {
        return new MySqlCommand(sql, conn);
    }
    public static MySqlCommand ObtenerComando(MySqlConnection conn, string sql, MySqlTransaction tran)
    {
        return new MySqlCommand(sql, conn, tran);
    }
    public bool EjecutarProcedimiento(string pNombre)
    {
        MySqlConnection conexion = ObtenerConexion();
        try
        {
            MySqlCommand cmdProc = new MySqlCommand(pNombre, conexion);
            cmdProc.CommandType = CommandType.StoredProcedure;
            if (conexion.State != ConnectionState.Open)
                conexion.Open();
            cmdProc.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            conexion.Close();
            MySqlConnection.ClearPool(conexion);
        }
    }
}