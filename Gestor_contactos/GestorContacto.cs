namespace Gestor_contactos;

public class GestorContacto
{
    private List<Contacto> contactos = new List<Contacto>();
    private RepositorioContacto connect;
    public GestorContacto(RepositorioContacto connect)
    {
        this.connect = connect;
    }
    public ResultadoOperacion AgregarContacto()
    {
        string nombre = Validador.ValidarString("Nombre: ");
        if (!Validador.EsNombreUnico(contactos, nombre))
        {
            return new ResultadoOperacion(false, $"❌ Ya existe un contacto con el nombre {nombre}");
        }

        string telefono = Validador.ValidarString("Telefono: ");
        string email = Validador.ValidarString("Email: ");
        if (!Validador.EsEmailUnico(contactos, email))
        {
            return new ResultadoOperacion(false, $"❌ Ya existe un contacto con el email {email}");
        }

        contactos.Add(new Contacto
        {
            Nombre = nombre,
            Telefono = telefono,
            Email = email,
            FechaCreacion = DateTime.Now
        });
        connect.GuardarContactos(contactos);
        return new ResultadoOperacion(true, $"✅ El contacto se agrego correctamente.");
    }

    private void MostrarContacto(Contacto contacto, int indice)
    {
        Console.WriteLine($"{indice} -> Nombre: {contacto.Nombre}, Teléfono: {contacto.Telefono}, Email: {contacto.Email}");
    }
    private void MostrarContacto(Contacto contacto)
    {
        Console.WriteLine($"-> Nombre: {contacto.Nombre}, Teléfono: {contacto.Telefono}, Email: {contacto.Email}");
    }
    private void MostrarResultadosXIndice(List<Contacto> contactos)
    {
        int indice = 1;
        foreach (var contacto in contactos)
        {
            MostrarContacto(contacto, indice);
            indice++;
        }
    }
    private void MostrarResultados(List<Contacto> contactos)
    {
        foreach (var contacto in contactos)
        {
            MostrarContacto(contacto);
        }
    }


    public ResultadoOperacion ModificarContacto()
    {
        string busqueda = Validador.ValidarString("Ingrese el nombre del contacto a buscar.");
        var resultado = Buscar(busqueda);

        if (resultado.Any())
        {
            int indice;
            MostrarResultadosXIndice(resultado);

            indice = Validador.ValidarInt("Elija el contacto a modificar seleccionando por su indice");
            if (Validador.ValidarIndice(indice, resultado.Count()))
            {
                var c = resultado[indice - 1];
                MostrarContacto(c, indice);
                if (Validador.ResponderSiONo("Desea modificar el contacto S/N"))
                {
                    c.Nombre = Validador.ValidarCambio("Ingrese un NOMBRE NUEVO. Si no ingresa nada, se conserva el valor anterior.", c.Nombre);

                    c.Telefono = Validador.ValidarCambio("Ingrese un TELEFONO NUEVO. Si no ingresa nada, se conserva el valor anterior.", c.Telefono);
                    c.Email = Validador.ValidarCambio("Ingrese un EMAIL NUEVO. Si no ingresa nada, se conserva el valor anterior.", c.Email);

                    connect.GuardarContactos(contactos);
                    return new ResultadoOperacion(true, $"Datos modificados con exito! \nLos datos quedaron asi: \n Nombre: {c.Nombre} \nTeléfono: {c.Telefono} \n Email: {c.Email}");
                }
                else
                {
                    return new ResultadoOperacion(false, "❌ Ninguna opcion elegida.");
                }
            }
            return new ResultadoOperacion(false, "❌ Indice fuera de rango");
        }
        else
        {
            return new ResultadoOperacion(false, "❌ No se encontro ninguna coincidencia.");
        }
    }
    public ResultadoOperacion ListarContactos()
    {
        if (contactos.Count() == 0)
        {
            return new ResultadoOperacion(false, "No hay contactos.");
        }
        else
        {
            MostrarResultadosXIndice(contactos);
            return new ResultadoOperacion(true, "✅ Contactos cargados correctamente");
        }
    }


    public void CargarContactos()
    {
        ResultadoCarga resultadoCarga = connect.CargarContactos();
        if (resultadoCarga.Exito)
        {
            string[] lineas = resultadoCarga.Lineas;
            Console.WriteLine(resultadoCarga.Mensaje);
            foreach (var linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    string[] datos = linea.Split(";");
                    try
                    {
                        if (datos.Length >= 4)
                        {
                            // DateTime fechaGuardada = DateTime.TryParse(datos[3], out DateTime x) ? x : DateTime.Now;
                            DateTime fechaGuardada = DateTime.Parse(datos[3]);
                            var contacto = new Contacto { Nombre = datos[0], Telefono = datos[1], Email = datos[2], FechaCreacion = fechaGuardada };

                            contactos.Add(contacto);
                        }
                        else
                        {
                            Console.WriteLine($"❌Linea invalida: {linea}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌Error. Linea invalida: '{linea}': {ex.Message}");
                    }

                }
            }
        }
        else
        {
            Console.WriteLine(resultadoCarga.Mensaje);
        }
    }

    public ResultadoOperacion BuscarContacto()
    {
        string busqueda = Validador.ValidarString("Ingrese el nombre o número del contacto a buscar: ");
        if (string.IsNullOrWhiteSpace(busqueda))
        {
            return new ResultadoOperacion(false, "❌ Debe ingresar un nombre o número validos.");
        }
        var resultado = Buscar(busqueda);
        if (resultado.Any())
        {
            foreach (var i in resultado)
            {
                MostrarContacto(i);
            }
        }
        else
        {
            return new ResultadoOperacion(false, "No se encontraron coincidencias.");
        }
        return new ResultadoOperacion(true, "✅ Operacion exitosa.");

    }

    private List<Contacto> Buscar(string valorBuscado)
    {
        var resultado = contactos.Where(c => c.Nombre.Contains(valorBuscado, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(valorBuscado, StringComparison.OrdinalIgnoreCase)).ToList();
        return resultado;
    }
    public ResultadoOperacion EliminarContacto()
    {
        string busqueda = Validador.ValidarString("Ingrese el nombre o número del contacto a eliminar");
        var resultado = Buscar(busqueda);

        if (resultado.Any())
        {
            foreach (var i in resultado)
            {
                MostrarContacto(i);
            }

            if (Validador.Opcion1OOpcion2(resultado, "1"))
            {
                var primerContacto = resultado.First();
                System.Console.WriteLine("El contacto a eliminar es: ");
                MostrarContacto(primerContacto);

                if (Validador.ResponderSiONo("Desea eliminarlo? S/N"))
                {
                    contactos.Remove(primerContacto);
                    connect.GuardarContactos(contactos);
                }
                return new ResultadoOperacion(true, $"✅ Contacto {primerContacto.Nombre} eliminado!");
            }
            else if (Validador.Opcion1OOpcion2(resultado, "2"))
            {
                if (Validador.ResponderSiONo($"Esta seguro de borrar los {resultado.Count()} contactos? S/N"))
                {
                    contactos.RemoveAll(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || c.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase));
                    connect.GuardarContactos(contactos);
                }
                return new ResultadoOperacion(true, $"✅ {resultado.Count()} contactos fueron eliminados.");
            }
            else
            {
                return new ResultadoOperacion(false, "❌ Ninguna opcion elegida.");
            }
        }
        else
        {
            return new ResultadoOperacion(false, "❌ No se encontro ninguna coincidencia.");
        }






    }
}
