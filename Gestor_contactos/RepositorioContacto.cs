using System.Collections;
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
                Console.WriteLine($"✅ {contactos.Count()} contactos guardados correctamente.");
            }

        }
        catch (System.Exception)
        {
            Console.WriteLine("❌ Error a la hora de guardar los contactos. Intente nuevamente");
        }

    }

    public ResultadoCarga CargarContactos()
    {
        try
        {
            if (!File.Exists(archivoDB))
            {
                return new ResultadoCarga(false, "❌El archivo no existe", new string[0]);
            }       
            string[] lineas = File.ReadAllLines(archivoDB);
            return new ResultadoCarga(true, "✅ Carga exitosa", lineas);

        }
        catch (Exception ex)
        {
            System.Console.WriteLine("❌No se puede acceder al DB.");
            return new ResultadoCarga(false, $"❌ Error. {ex.Message}", new string[0]);
            }

    }
}
