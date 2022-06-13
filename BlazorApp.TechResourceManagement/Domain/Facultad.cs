namespace BlazorApp.TechResourceManagement.Domain
{
    public class Facultad
    {
        //Variables
        private string _nombre { get; set; }
        private string _descripcion { get; set; }
        private PersonalCientifico _responsableCyT { get; set; }
        private IList<CentroDeInvestigacion> _centrosDeInvestigacion { get; set; }
        //Constructor
        public Facultad(string nombre, string descripcion, PersonalCientifico responsableCyT,
            IList<CentroDeInvestigacion> centrosDeInvestigacion)
        {
            _nombre = nombre;
            _descripcion = descripcion;
            _responsableCyT = responsableCyT;
            _centrosDeInvestigacion = centrosDeInvestigacion;
        }
        //Metodos
        public string MostrarFacultad()
        {
            return $"{_nombre} - {_descripcion}";
        }
    }
}
