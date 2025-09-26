namespace Gestor_contactos;

public class Contacto
{
    private static int IdCont { get; set; } //para agregar despues un id
    public int Id { get; set; } //para agregar despues
    public string Nombre { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Email { get; set; } = "";
}

