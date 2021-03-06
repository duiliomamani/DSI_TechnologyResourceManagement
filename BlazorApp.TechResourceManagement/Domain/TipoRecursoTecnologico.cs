namespace BlazorApp.TechResourceManagement.Domain
{
    public class TipoRecursoTecnologico
    {
        //Variables
        private string nombre { get; set; }
        private string descripcion { get; set; }
        private IList<Caracteristica> caracteristicas { get; set; }

        //Constructor
        public TipoRecursoTecnologico(string nombre, string descripcion, IList<Caracteristica> caracteristicas)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.caracteristicas = caracteristicas;
        }
        //Metodos
        public string Nombre { get => nombre; }
        public TipoRecursoTecnologico MostrarCategoria() => this;
        public bool EsTipoActual(string nombre)
        {
            return this.nombre == nombre;
        }
        public bool EsTipoSeleccionado(TipoRecursoTecnologico tipoRecursoTecnologico)
        {
            return nombre == tipoRecursoTecnologico.nombre;
        }
    }
}
