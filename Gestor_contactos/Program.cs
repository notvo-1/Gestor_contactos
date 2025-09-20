
namespace Gestor_contactos
{
    class Program
    {
        static List<Contacto> contactos = new List<Contacto>();
        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Gestor de Contactos ===");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Listar contactos");
                Console.WriteLine("3. Salir");
                Console.Write("Elige una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch(opcion)
                {
                    case "1":
                        AgregarContacto();
                        break;
                    case "2":
                        ListarContactos();
                        break;
                    case "3":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
                if (continuar)
                {
                    Console.WriteLine("Presiona ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        static void AgregarContacto()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";

            contactos.Add(new Contacto { Nombre = nombre, Telefono = telefono });

            Console.WriteLine("Contacto agregado.");
        }

        static void ListarContactos()
        {
            Console.WriteLine("=== Lista de Contactos ===");
            if (contactos.Count == 0)
            {
                Console.WriteLine("No hay contactos.");
            }
            else
            {
                foreach (var c in contactos)
                {
                    Console.WriteLine($"Nombre: {c.Nombre}, Teléfono: {c.Telefono}");
                }
            }
        }
    }

    class Contacto
    {
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
    }
}
