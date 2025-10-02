public class ResultadoOperacion
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; }

    public ResultadoOperacion(bool exito, string mensaje)
    {
        Exito = exito;
        Mensaje = mensaje;   
    }
    
}