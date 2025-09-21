
using System.Runtime.InteropServices;

namespace Gestor_contactos
{
    class Program
    {
        static List<Contacto> contactos = new List<Contacto>();
        const string archivoDB = "D:\\salvar\\Documentos\\code\\Gestor_contactos\\contactos.txt";
        static bool viewActualizado = false;
        static bool contactoGuardado = false;
        static void Main(string[] args)
        {
            bool continuar = true;
            CargarContactos();
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Gestor de Contactos ===");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Listar contactos");
                Console.WriteLine("3. Instancia de Prueba");
                Console.WriteLine("4.");//pendiente para nuevas opciones
                Console.WriteLine("5.");//pendiente para nuevas opciones
                Console.WriteLine("6. Salir");
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
                        InstanciaPrueba();
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        continuar = CheckGuardado();
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

            Program.contactoGuardado = false;
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
                foreach (var contacto in contactos)
                {
                    //aca pasa lo mismo, estoy iterando en la lista de objetos de la clase contacto por ende todo lo que itero son objetos y puedo acceder a sus atributos y metodos.
                    Console.WriteLine($"Nombre: {contacto.Nombre}, Teléfono: {contacto.Telefono}, Email: {contacto.Email}");
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

        static void GuardarContactos()
        {
            int archivoTamaño = File.ReadAllLines(archivoDB).Length;
            int contactosTamaño = Program.contactos.Count;
            if (archivoTamaño != contactosTamaño && Program.viewActualizado)
            {
                var lineas = new List<string>();
                foreach (var contacto in contactos)
                {
                    lineas.Add($"{contacto.Nombre};{contacto.Telefono};{contacto.Email}"); //claro puedo acceder directamenta al atributo o propiedad porque contactos es una lista de objetos de tipo Contacto. Es decir que en cada iteracion estoy tomando un objeto de tipo contacto ya instancidao (por eso objeto...) entonces puedo acceder a sus metodos y atributos (propiedades)
                }
                //sobre escribo todo el txt ahora
                File.WriteAllLines(archivoDB, lineas);
                Console.WriteLine($"✅ {contactos.Count} contactos guardados correctamente.");
                Program.contactoGuardado = true;
                Program.viewActualizado = false;

            }
            else
            {
                System.Console.WriteLine("Se deben cargar los datos primero.");
            }
        }

        static bool CheckGuardado()
        {
            if (!contactoGuardado)
            {
                GuardarContactos();
                return false;
            }
            return true;
        }
        static void CargarContactos()
        {
            if (File.Exists(archivoDB) && !Program.viewActualizado)
            {
                string[] lineas = File.ReadAllLines(archivoDB);

                foreach (var linea in lineas)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        string[] datos = linea.Split(";");

                        if (datos.Length >= 3)
                        {
                            contactos.Add(new Contacto
                            {
                                Nombre = datos[0],
                                Telefono = datos[1],
                                Email = datos[2]
                            });
                        }
                    }
                }
                if (lineas.Length == contactos.Count)
                {
                    Program.viewActualizado = true; //quenteligente
                }
                else
                {
                    System.Console.WriteLine("Algo salio mal en la carga del DB. Lo arreglo despues xD");
                }
            }
            else
            {
                System.Console.WriteLine("No se puede acceder al DB.");
            }
        }
    }

    class Contacto
    {
        private static int IdCont { get; set; } //para agregar despues un id
        public int Id { get; set; } //para agregar despues
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
