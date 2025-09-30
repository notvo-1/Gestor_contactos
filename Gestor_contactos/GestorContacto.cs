namespace Gestor_contactos;

public class GestorContacto
{
    private List<Contacto> contactos = new List<Contacto>();
    private RepositorioContacto connect;
    public GestorContacto(RepositorioContacto connect) {
        this.connect = connect;
    }
    public void AgregarContacto()
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        contactos.Add(new Contacto { Nombre = nombre, Telefono = telefono, Email = email, FechaCreacion = DateTime.Now });

        connect.GuardarContactos(contactos);
        Console.WriteLine("Contacto agregado.");
    }

    public void ModificarContacto()
    {
        string busqueda = Validador.ValidarString("Ingrese el nombre del contacto a buscar.");
        var resultado = Buscar(busqueda);

        if (resultado.Any())
        {
            System.Console.WriteLine("Los resultados que coinciden con la busqueda son: ");
            System.Console.WriteLine($"Se han encontrado {resultado.Count()} resultados.");
            int indice = 1;
            var contactosPorModificar = new List<Contacto>();
            foreach (var contacto in resultado)
            {
                Console.WriteLine($"{indice} -> Nombre: {contacto.Nombre}, Teléfono: {contacto.Telefono}, Email: {contacto.Email}");
                indice++;
                contactosPorModificar.Add(contacto);
            }

            indice = Validador.ValidarInt("Elija el contacto a modificar seleccionando por su indice");
            if (Validador.ValidarIndice(indice, resultado.Count()))
            {
                var c = contactosPorModificar[indice - 1];
                Console.WriteLine($"Nombre: {c.Nombre}, Teléfono: {c.Telefono}, Email: {c.Email}");
                System.Console.WriteLine("Desea modificar el contacto? S/N");
                string opcion = (Console.ReadLine() ?? "").ToLower();

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
                    connect.GuardarContactos(contactos);
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
            Console.WriteLine("Indice fuera de rango");
            return;
        }
        else
        {
            System.Console.WriteLine("❌ No se encontro ninguna coincidencia.");
            return;
        }
   }
    public void ListarContactos()
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
                Console.WriteLine($"Nombre: {contacto.Nombre}, Teléfono: {contacto.Telefono}, Email: {contacto.Email}, Fecha Creacion: {contacto.FechaCreacion}");
            }
        }
    }


    public void CargarContactos()
    {
        connect.CargarContactos(contactos);
    }

    public void BuscarContacto()
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

    private List<Contacto> Buscar(string valorBuscado)
    {
        var resultado = contactos.Where(c => c.Nombre.Contains(valorBuscado, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(valorBuscado, StringComparison.OrdinalIgnoreCase)).ToList();
        return resultado;
    }

    public void EliminarContacto()
    {
        string busqueda = Validador.ValidarString("Ingrese el nombre o número del contacto a eliminar");
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
                    connect.GuardarContactos(contactos);
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
                    connect.GuardarContactos(contactos);
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
