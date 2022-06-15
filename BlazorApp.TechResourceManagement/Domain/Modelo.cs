namespace BlazorApp.TechResourceManagement.Domain
{
    public class Modelo
    {
        //Variables privadas
        private string nombre { get; set; }
        //Getter
        public string Nombre { get => nombre; }
        //Constructor
        public Modelo(string nombre)
        {
            this.nombre = nombre;
        }
        //Metodos
        public Modelo MostrarModelo() => this;

        public bool EsTuMarca(Marca marca)
        {
            return marca.EsTuMarca(this);
        }
    }
}
