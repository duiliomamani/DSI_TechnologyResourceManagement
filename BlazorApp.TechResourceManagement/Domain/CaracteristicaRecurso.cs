namespace BlazorApp.TechResourceManagement.Domain
{
    public class CaracteristicaRecurso
    {
        //Variables
        private string valor { get; set; }
        private Caracteristica caracteristica { get; set; }
        //Constructor
        public CaracteristicaRecurso(string valor, Caracteristica caracteristica)
        {
            this.valor = valor;
            this.caracteristica = caracteristica;
        }
        //Metodos
        public string MostrarCategoriaRecurso()
        {
            return string.Empty;
        }

    }
}
