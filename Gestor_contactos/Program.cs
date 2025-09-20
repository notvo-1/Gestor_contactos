
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
                Console.WriteLine("4. Instancia de Prueba");
                Console.Write("Elige una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
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
                    case "4":
                        InstanciaPrueba();
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

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            contactos.Add(new Contacto { Nombre = nombre, Telefono = telefono, Email = email });

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
                    Console.WriteLine($"Nombre: {c.Nombre}, Teléfono: {c.Telefono}, Email: {c.Email}");
                }
            }
        }

        static void InstanciaPrueba()
        {
            Contacto c1 = new Contacto();
            c1.Nombre = "Matias";
            c1.Telefono = "1234";
            c1.Email = "m@gmail.com";

            Console.WriteLine($"Nombre: {c1.Nombre}, Telefono: {c1.Telefono}, Email: {c1.Email}");
        }
    }

    class Contacto
    {
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
