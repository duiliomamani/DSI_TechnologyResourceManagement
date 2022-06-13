namespace BlazorApp.TechResourceManagement.Domain
{
    public class Marca
    {
        //Variables
        private string _nombre { get; set; }
        public string Nombre { get => _nombre; }
        private IList<Modelo> _modelos { get; set; }
        public IList<Modelo> Modelos { get => _modelos; } 
        //Constructor
        public Marca(string nombre, IList<Modelo> modelos)
        {
            _nombre = nombre;
            _modelos = modelos;
        }
        //Metodos
        public string MostrarMarca() => _nombre;
        public IList<Modelo> MostrarMisModelos() => _modelos;
    }
}
