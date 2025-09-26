
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace Gestor_contactos
{
    class Program
    {

        static void Main(string[] args)
        {
            bool continuar = true;
            GestorContacto.CargarContactos();
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
                        GestorContacto.AgregarContacto();
                        break;
                    case "2":
                        GestorContacto.ListarContactos();
                        break;
                    case "3":
                        GestorContacto.ModificarContacto();
                        break;
                    case "4":
                        GestorContacto.BuscarContacto();
                        break;
                    case "5":
                        GestorContacto.EliminarContacto();
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
    }
}
