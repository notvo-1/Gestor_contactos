namespace Gestor_contactos;

public class RepositorioContacto
{
    const string archivoDB = "contactos.txt";

    private void GuardarContactos()
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

}
