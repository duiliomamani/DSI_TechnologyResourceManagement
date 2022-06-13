namespace BlazorApp.TechResourceManagement.Domain
{
    public class Facultad
    {
        //Variables
        private string nombre { get; set; }
        private string descripcion { get; set; }
        private PersonalCientifico responsableCyT { get; set; }
        private IList<CentroDeInvestigacion> centrosDeInvestigacion { get; set; }
        //Constructor
        public Facultad(string nombre, string descripcion, PersonalCientifico responsableCyT,
            IList<CentroDeInvestigacion> centrosDeInvestigacion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.responsableCyT = responsableCyT;
            this.centrosDeInvestigacion = centrosDeInvestigacion;
        }
        //Metodos
        public string MostrarFacultad()
        {
            return $"{nombre} - {descripcion}";
        }
    }
}
