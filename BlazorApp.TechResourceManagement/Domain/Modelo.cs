namespace BlazorApp.TechResourceManagement.Domain
{
    public class Modelo
    {
        //Variables
        private string _nombre { get; set; }
        public string Nombre { get => _nombre; }
        //Constructor
        public Modelo(string nombre)
        {
            _nombre = nombre;
        }
        //Metodos
        public string MostrarModelo() => _nombre;
    }
}
