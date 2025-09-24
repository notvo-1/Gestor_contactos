
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace Gestor_contactos
{
    class Program
    {
        static List<Contacto> contactos = new List<Contacto>();
        const string archivoDB = "contactos.txt";
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
                Console.WriteLine("4. Buscar Contacto");
                Console.WriteLine("5. Elminar Contacto");//pendiente para nuevas opciones
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
                        BuscarContacto();
                        break;
                    case "5":
                        EliminarContacto();
                        break;
                    case "6":
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

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            contactos.Add(new Contacto { Nombre = nombre, Telefono = telefono, Email = email });

            GuardarContactos();
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
            if (archivoTamaño != contactosTamaño)
            {
                var lineas = new List<string>();
                foreach (var contacto in contactos)
                {
                    lineas.Add($"{contacto.Nombre};{contacto.Telefono};{contacto.Email}"); //claro puedo acceder directamenta al atributo o propiedad porque contactos es una lista de objetos de tipo Contacto. Es decir que en cada iteracion estoy tomando un objeto de tipo contacto ya instancidao (por eso objeto...) entonces puedo acceder a sus metodos y atributos (propiedades)
                }
                //sobre escribo todo el txt ahora
                File.WriteAllLines(archivoDB, lineas);
                Console.WriteLine($"✅ {contactos.Count} contactos guardados correctamente.");
            }
            else
            {
                System.Console.WriteLine("Se deben cargar los datos primero.");
            }
        }


        static void CargarContactos()
        {
            if (File.Exists(archivoDB))
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

            }
            else
            {
                System.Console.WriteLine("No se puede acceder al DB.");
            }
        }

        static void BuscarContacto()
        {
            string busqueda;
            System.Console.WriteLine("Ingrese el nombre o número del contacto a buscar: ");
            busqueda = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                System.Console.WriteLine("❌ Debe ingresar un nombre o número validos.");
                return;
            }
            var resultado = contactos.Where(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase)).ToList();
            if (resultado.Any())
            {
                foreach (var i in resultado)
                {
                    Console.WriteLine($"Nombre: {i.Nombre}, Teléfono: {i.Telefono}, Email: {i.Email}");
                }

            }
            else
            {
                System.Console.WriteLine("No se encontraron coincidencias.");
            }

        }

        static void EliminarContacto()
        {
            System.Console.WriteLine("Quiere hacer una eliminacion multiple? S/N");
            string opcion = Console.ReadLine() ?? "";

            if (opcion.ToLower() == "s")
            {
                System.Console.WriteLine("Ingrese el nombre o número del contacto a eliminar: ");
                string busqueda = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    System.Console.WriteLine("❌ Debe ingresar un nombre o número validos.");
                    return;
                }
                var resultado = contactos.Where(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase)).ToList();
                if (resultado.Any())
                {
                    System.Console.WriteLine("Los resultados que coinciden con la busqueda son: ");
                    foreach (var i in resultado)
                    {
                        Console.WriteLine($"Nombre: {i.Nombre}, Teléfono: {i.Telefono}, Email: {i.Email}");
                    }
                    System.Console.WriteLine($"Desea eliminar los {resultado.Count()} registros? S/N");
                    opcion = Console.ReadLine() ?? "".ToLower();

                    if (opcion.ToLower() == "s")
                    {
                        contactos.RemoveAll(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase));

                        System.Console.WriteLine($"✅ {resultado.Count()} contactos fueron eliminados.");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    System.Console.WriteLine($"No se encontraron coincidencias para {busqueda}");
                }
            }
            else if (opcion == "n")
            {
                System.Console.WriteLine("Ingrese el nombre o número del contacto a eliminar: ");
                string busqueda = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    System.Console.WriteLine("❌ Debe ingresar un nombre o número validos.");
                    return;
                }
                var resultado = contactos.Where(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase)).ToList();

                if (resultado.Any())
                {
                    var contacto = resultado.FirstOrDefault();
                    Console.WriteLine($"Nombre: {contacto?.Nombre}, Teléfono: {contacto?.Telefono}, Email: {contacto?.Email}");

                    System.Console.WriteLine("Desea eliminar el contacto? S/N");
                    opcion = Console.ReadLine() ?? "".ToLower();

                    if (opcion == "s" && contacto != null)
                    {
                        contactos.Remove(contacto);
                        System.Console.WriteLine($"✅ Contacto {contacto.Nombre} eliminado.");
                        GuardarContactos();

                    }
                    else
                    {
                        System.Console.WriteLine("❌ No se puede eliminar. Intente nuevamente.");
                    }

                }
            }
            else
            {
                return;
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
