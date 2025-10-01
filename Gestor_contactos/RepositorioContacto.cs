using System.Reflection.Metadata;

namespace Gestor_contactos;

public class RepositorioContacto
{
    const string archivoDB = "contactos.txt";

    public void GuardarContactos(List<Contacto> contactos)
    {
        try
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
        catch (System.Exception)
        {
            Console.WriteLine("❌ Error a la hora de guardar los contactos. Intente nuevamente");
        }

    }

    public string[] CargarContactos()
    {
        try
        {
            string[] lineas = File.ReadAllLines(archivoDB);
            Console.WriteLine("✅ Archivos cargados con exito");
            return lineas;
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("❌No se puede acceder al DB.");
            return new string[0];
        }

    }
}
