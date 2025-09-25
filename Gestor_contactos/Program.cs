
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
                Console.WriteLine("3. Modificar Contacto");
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
                        ModificarContacto();
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

        static void ModificarContacto()
        {
            System.Console.WriteLine("Ingrese el nombre o número del contacto a modificar: ");
            string busqueda = Console.ReadLine() ?? "";
            while (true)
            {
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    System.Console.WriteLine("❌ Debe ingresar un nombre o número validos.");
                    System.Console.WriteLine("Ingrese el nombre o número del contacto a modificar: ");
                    busqueda = Console.ReadLine() ?? "";
                }
                else
                {
                    break;
                }
            }
            var resultado = Buscar(busqueda);

            if (resultado.Any())
            {
                System.Console.WriteLine("Los resultados que coinciden con la busqueda son: ");
                System.Console.WriteLine($"Se han encontrado {resultado.Count()} resultados.");
                int indice = 1;
                var contactosPorModificar =  new List<Contacto>();
                foreach (var contacto in resultado)
                {
                    Console.WriteLine($"{indice} -> Nombre: {contacto.Nombre}, Teléfono: {contacto.Telefono}, Email: {contacto.Email}");
                    indice++;
                    contactosPorModificar.Add(contacto);
                }
                System.Console.WriteLine("Elija el contacto a modificar seleccionando por su indice.");

                string opcion = Console.ReadLine() ?? "";

                var c = contactosPorModificar[int.Parse(opcion)-1];
                Console.WriteLine($"Nombre: {c.Nombre}, Teléfono: {c.Telefono}, Email: {c.Email}");
                System.Console.WriteLine("Desea modificar el contacto? S/N");
                opcion = (Console.ReadLine() ?? "").ToLower();

                if (opcion == "s")
                {
                    System.Console.WriteLine("Ingrese un NOMBRE NUEVO. Si no ingresa nada, se conserva el valor anterior.");
                    string nuevoNombre = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(nuevoNombre))
                    {
                        c.Nombre = nuevoNombre;
                    }
                    System.Console.WriteLine("Ingrese un TELEFONO NUEVO. Si no ingresa nada, se conserva el valor anterior.");
                    string nuevoTelefono = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(nuevoTelefono))
                    {
                        c.Telefono = nuevoTelefono;
                    }
                    System.Console.WriteLine("Ingrese un EMAIL NUEVO. Si no ingresa nada, se conserva el valor anterior.");
                    string nuevoEmail = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(nuevoEmail))
                    {
                        c.Email = nuevoEmail;
                    }

                    System.Console.WriteLine("Datos modificados con exito!");
                    System.Console.WriteLine("El contacto ha quedado asi: ");
                    Console.WriteLine($"Nombre: {c.Nombre}, Teléfono: {c.Telefono}, Email: {c.Email}");
                    GuardarContactos();
                }
                else if (opcion == "n")
                {
                    System.Console.WriteLine($"Modificacion cancelada");
                    return;
                }
                else
                {
                    System.Console.WriteLine("❌ Ninguna opcion elegida.");
                    return;
                }
            }
            else
            {
                System.Console.WriteLine("❌ No se encontro ninguna coincidencia.");
                return;
            }
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
            if (contactos.Count() > 0)
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
            var resultado = Buscar(busqueda);
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

        static List<Contacto> Buscar(string valorBuscado)
        {
            var resultado = contactos.Where(c => c.Nombre.Contains(valorBuscado, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(valorBuscado, StringComparison.OrdinalIgnoreCase)).ToList();
            return resultado;
        }

        static void EliminarContacto()
        {
            System.Console.WriteLine("Ingrese el nombre o número del contacto a eliminar: ");
            string busqueda = Console.ReadLine() ?? "";
            while (true)
            {
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    System.Console.WriteLine("❌ Debe ingresar un nombre o número validos.");
                    System.Console.WriteLine("Ingrese el nombre o número del contacto a eliminar: ");
                    busqueda = Console.ReadLine() ?? "";
                }
                else
                {
                    break;
                }
            }
            var resultado = Buscar(busqueda);

            if (resultado.Any())
            {
                System.Console.WriteLine("Los resultados que coinciden con la busqueda son: ");
                foreach (var i in resultado)
                {
                    Console.WriteLine($"Nombre: {i.Nombre}, Teléfono: {i.Telefono}, Email: {i.Email}");
                }

                System.Console.WriteLine($"Desea eliminar 1 contacto o los {resultado.Count()}?");
                System.Console.WriteLine("1 - Eliminar el primer contacto");
                System.Console.WriteLine("2 - Eliminar todos los resultados");
                string opcion = Console.ReadLine() ?? "";

                if (opcion == "1")
                {
                    var primerContacto = resultado.First();
                    System.Console.WriteLine("El contacto a eliminar es: ");
                    System.Console.WriteLine($"Nombre: {primerContacto.Nombre}, Telefono: {primerContacto.Telefono}, Email: {primerContacto.Email}");

                    System.Console.WriteLine("Desea eliminarlo? S/N");
                    string confirmacion = (Console.ReadLine() ?? "").ToLower();

                    if (confirmacion == "s")
                    {
                        contactos.Remove(primerContacto);
                        System.Console.WriteLine($"✅ Contacto {primerContacto.Nombre} eliminado!");
                        GuardarContactos();
                    }
                }
                else if (opcion == "2")
                {
                    System.Console.WriteLine($"Esta seguro de borrar los {resultado.Count()} contactos? S/N");
                    string confirmacion = (Console.ReadLine() ?? "").ToLower();
                    if (confirmacion == "s")
                    {
                        contactos.RemoveAll(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase));
                        System.Console.WriteLine($"✅ {resultado.Count()} contactos fueron eliminados.");
                        GuardarContactos();
                    }
                }
                else
                {
                    System.Console.WriteLine("❌ Ninguna opcion elegida.");
                    return;
                }
            }
            else
            {
                System.Console.WriteLine("❌ No se encontro ninguna coincidencia.");
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
