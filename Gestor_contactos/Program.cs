

namespace Gestor_contactos
{
    class Program
    {

        static void Main(string[] args)
        {
            RepositorioContacto repositorio = new RepositorioContacto();
            GestorContacto gestor = new GestorContacto(repositorio);
            InterfazConsola menu = new InterfazConsola(gestor);
            menu.Menu();
        }
    }
}
