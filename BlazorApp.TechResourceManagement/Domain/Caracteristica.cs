namespace BlazorApp.TechResourceManagement.Domain
{
    public class Caracteristica
    {
        //Variables
        private string nombre { get; set; }
        private string descripcion { get; set; }
        //Constructor
        public Caracteristica(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }
        //Metodos
        public string MostrarCaracteristica()
        {
            return $"{nombre} - {descripcion}";
        }
    }
}
