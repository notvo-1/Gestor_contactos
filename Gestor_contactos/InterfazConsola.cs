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
        ResultadoOperacion resultado;
        bool continuar = true;
        gestor.CargarContactos();
        while (continuar)
        {
            Console.WriteLine("=== Gestor de Contactos ===");
            Console.WriteLine("1. Agregar contacto");
            Console.WriteLine("2. Listar contactos");
            Console.WriteLine("3. Modificar Contacto");
            Console.WriteLine("4. Buscar Contacto");
            Console.WriteLine("5. Elminar Contacto");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");

            string opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    resultado = gestor.AgregarContacto();
                    Console.WriteLine(resultado.Mensaje);
                    break;
                case "2":
                    Console.WriteLine("=== Lista de Contactos ===");
                    resultado = gestor.ListarContactos();
                    Console.WriteLine(resultado.Mensaje);
                    break;
                case "3":
                    resultado = gestor.ModificarContacto();
                    Console.WriteLine(resultado.Mensaje);
                    break;
                case "4":
                    resultado = gestor.BuscarContacto();
                    Console.WriteLine(resultado.Mensaje);
                    break;
                case "5":
                    Console.WriteLine("Los resultados que coinciden con la busqueda son: ");
                    resultado = gestor.EliminarContacto();
                    Console.WriteLine(resultado.Mensaje);
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
                Console.Clear();
            }
        }
    }
}

