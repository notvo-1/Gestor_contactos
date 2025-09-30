namespace Gestor_contactos;

public class InterfazConsola
{
    GestorContacto gestor;
    public InterfazConsola(GestorContacto gestor)
    {
        this.gestor = gestor;
    }
    public void Menu()
    {

        bool continuar = true;
        gestor.CargarContactos();
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
                    gestor.AgregarContacto();
                    break;
                case "2":
                    gestor.ListarContactos();
                    break;
                case "3":
                    gestor.ModificarContacto();
                    break;
                case "4":
                    gestor.BuscarContacto();
                    break;
                case "5":
                    gestor.EliminarContacto();
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

