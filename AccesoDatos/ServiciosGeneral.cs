namespace AccesoDatos;
using System.Security.Cryptography;
using System.Text;
public class ServiciosGeneral
{
    public string PasswordToSha256(string input)
    {
        SHA256 sha256 = SHA256.Create();
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                output.Append(hashBytes[i].ToString("X2"));
            }
            return output.ToString();
        }
    }

    public string GenerarPasswordDefaul(string nombre, string telefono)
    {
        //Retorna un hash256 como password tomando los 3 primeros caracteres del nombre y del telefono
        var HashToString = PasswordToSha256(nombre.Remove(3) + telefono.Remove(3));
        return HashToString;
    }
}