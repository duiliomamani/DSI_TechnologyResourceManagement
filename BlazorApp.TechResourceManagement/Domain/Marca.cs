namespace BlazorApp.TechResourceManagement.Domain
{
    public class Marca
    {
        //Variables
        private string nombre { get; set; }
        private IList<Modelo> modelos { get; set; }
        //Constructor
        public Marca(string nombre, IList<Modelo> modelos)
        {
            this.nombre = nombre;
            this.modelos = modelos;
        }
        //Metodos
        public string MostrarMarca() => nombre;
        public IList<Modelo> MostrarMisModelos() => modelos;

        public bool EsTuModelo(Modelo modelo)
        {
            return modelos.Any(x => x.MostrarModelo() == modelo.MostrarModelo());
        }
    }
}
