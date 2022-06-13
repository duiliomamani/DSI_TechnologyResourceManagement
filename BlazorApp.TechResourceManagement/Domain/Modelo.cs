namespace BlazorApp.TechResourceManagement.Domain
{
    public class Modelo
    {
        //Variables
        private string nombre { get; set; }
        //Constructor
        public Modelo(string nombre)
        {
            this.nombre = nombre;
        }
        //Metodos
        public string MostrarModelo() => nombre;

        public bool EsTuMarca(Marca marca)
        {
            return marca.EsTuModelo(this);
        }
    }
}
