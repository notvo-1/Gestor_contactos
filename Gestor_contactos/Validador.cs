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
}