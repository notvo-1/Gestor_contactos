namespace Gestor_contactos;

public class Prueba
{
        private void InstanciaPrueba()
    {
        Contacto c1 = new Contacto();
        c1.Nombre = "Matias";
        c1.Telefono = "1234";
        c1.Email = "m@gmail.com";

        Console.WriteLine($"Nombre: {c1.Nombre}, Telefono: {c1.Telefono}, Email: {c1.Email}");
    }
}
