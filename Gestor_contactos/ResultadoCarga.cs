using Gestor_contactos;

public class ResultadoCarga
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = "";
    public string[] Lineas { get; set; }

    public ResultadoCarga(bool exito, string mensaje, string[] lineas)
    {
        Exito = exito;
        Mensaje = mensaje;
        Lineas = lineas;
    }
}