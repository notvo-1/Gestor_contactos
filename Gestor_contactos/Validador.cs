using Gestor_contactos;


public class Validador
{
    public static string ValidarString(string mensaje)
    {
        do
        {
            System.Console.WriteLine(mensaje + " :");
            string busqueda = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                System.Console.WriteLine("❌ Debe ingresar un nombre o número validos.");

            }
            else
            {
                return busqueda;
            }

        } while (true);

    }

    public static int ValidarInt(string mensaje)
    {
        do
        {
            Console.Write(mensaje + " :");

            if (int.TryParse(Console.ReadLine(), out int num))
            {
                return num;
            }

        } while (true);
    }
    public static bool ValidarIndice(int indice, int max)
    {
        return indice >= 1 && indice <= max;
    }

    public static string ValidarCambio(string mensaje, string variableOriginal)
    {
        Console.WriteLine(mensaje);
        string variable = Console.ReadLine() ?? "";

        if (!String.IsNullOrWhiteSpace(variable))
        {
            return variable;
        }
        return variableOriginal;
    }

    public static bool EsNombreUnico(List<Contacto> contactos, string nombre)
    {
        return !contactos.Any(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public static bool EsEmailUnico(List<Contacto> contactos, string email)
    {
        return !contactos.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public static bool ResponderSiONo(string mensaje)
    {
        string respuesta = ValidarString(mensaje);
        return respuesta == "s";
    }

    public static bool Opcion1OOpcion2(List<Contacto> resultado, string opcion)
    {
        string respuesta = ValidarString($"Desea eliminar 1 contacto o los {resultado.Count()}? \n 1- Eliminar el primer contacto \n 2-Eliminar todos los resultados.");
        return respuesta == opcion;
    }
}


