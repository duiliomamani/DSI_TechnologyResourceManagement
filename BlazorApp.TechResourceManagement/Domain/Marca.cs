namespace BlazorApp.TechResourceManagement.Domain
{
    public class Marca
    {
        //Variables
        private string nombre { get; set; }
        private IList<Modelo> modelos { get; set; }
        //Getter
        public string Nombre { get => nombre; }
        //Constructor
        public Marca(string nombre, IList<Modelo> modelos)
        {
            this.nombre = nombre;
            this.modelos = modelos;
        }
        //Metodos
        public Marca MostrarMarca() => this;
        public IList<Modelo> MostrarMisModelos() => modelos;

        public bool EsTuMarca(Modelo modelo)
        {
            return modelos.Any(x => x.MostrarModelo().Nombre == modelo.MostrarModelo().Nombre);
        }
    }
}
