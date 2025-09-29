using System.Reflection.Metadata;

namespace Gestor_contactos;

public class RepositorioContacto
{
    const string archivoDB = "contactos.txt";

    public void GuardarContactos(List<Contacto> contactos)
    {
        if (contactos.Count() > 0)
        {
            var lineas = new List<string>();
            foreach (var contacto in contactos)
            {
                lineas.Add($"{contacto.Nombre};{contacto.Telefono};{contacto.Email};{contacto.FechaCreacion}");
            }
            //sobre escribo todo el txt ahora
            File.WriteAllLines(archivoDB, lineas);
            Console.WriteLine($"✅ {contactos.Count} contactos guardados correctamente.");
        }
    }

    public void CargarContactos(List<Contacto> contactos)
    {
        if (File.Exists(archivoDB))
        {
            string[] lineas = File.ReadAllLines(archivoDB);

            foreach (var linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    string[] datos = linea.Split(";");

                    if (datos.Length >= 4)
                    {
                        DateTime fechaGuardada = DateTime.TryParse(datos[3], out DateTime x) ? x : DateTime.Now;
                        var contacto = new Contacto { Nombre = datos[0], Telefono = datos[1], Email = datos[2], FechaCreacion = fechaGuardada };

                        contactos.Add(contacto);
                    }
                }
            }

        }
        else
        {
            System.Console.WriteLine("No se puede acceder al DB.");
        }
    }
}
