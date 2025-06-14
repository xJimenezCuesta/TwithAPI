namespace PruebaTecnica.Models
{
    public class Excepcion
    {
        public string Error { get; set; }
        
        public Excepcion(string error)
        {
            Error = error;
        }

    }
}
