namespace Entidades;

public class Usuario
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string password { get; set; }
    public string username { get; set; }
    public string? token { get; set; }
    public string correo { get; set; }
    public string telefono { get; set; }
    public string apellidos { get; set; }
    public string direccion { get; set; }
    public bool active { get; set; }
    public int role { get; set; }
}
