namespace BlazorApp.TechResourceManagement.Domain
{
    public class CentroDeInvestigacion
    {
        private string _nombre { get; set; }    
        private string _sigla {  get; set; }
        private string _direccion { get; set; }
        private string _edificio { get; set; }
        private int _piso { get; set; }
        private string _coordenadas { get; set; }
        private IList<string> _telefonosContacto { get; set; }
        private string _correoElectronico { get; set; }
        private string _numeroResolucionCreacion { get; set; }
        private DateTime _fechaResolucionCreacion { get; set; }
        private string _reglamento { get; set; }
        private IList<string> _caracteristicasGenerales { get; set; }
        private DateTime _fechaAlta { get; set; } = DateTime.Now;
        private double _tiempoAntelacionReserva { get; set; }
        private DateTime? _fechaBaja { get; set; }
        private string _motivoBaja { get; set; }
        private IList<AsignacionDirectorCI> _directorDelCi { get; set; }
        private IList<AsignacionCientificoDelCI> _cientificos { get; set; }
        private IList<RecursoTecnologico> _recursosTecnologicos { get; set; }

        public CentroDeInvestigacion(string nombre, string sigla, string direccion, string edificio, int piso,
            string coordenadas, IList<string> telefonosContacto, string correoElectronico,
            string numeroResolucionCreacion, DateTime fechaResolucionCreacion, 
            string reglamento, IList<string> caracteristicasGenerales,
            DateTime fechaAlta, double tiempoAntelacionReserva, 
            DateTime? fechaBaja, string motivoBaja, 
            IList<AsignacionDirectorCI> directorDelCi,
            IList<AsignacionCientificoDelCI> cientificos, 
            IList<RecursoTecnologico> recursosTecnologicos)
        {
            _nombre = nombre;
            _sigla = sigla;
            _direccion = direccion;
            _edificio = edificio;
            _piso = piso;
            _coordenadas = coordenadas;
            _telefonosContacto = telefonosContacto;
            _correoElectronico = correoElectronico;
            _numeroResolucionCreacion = numeroResolucionCreacion;
            _fechaResolucionCreacion = fechaResolucionCreacion;
            _reglamento = reglamento;
            _caracteristicasGenerales = caracteristicasGenerales;
            _fechaAlta = fechaAlta;
            _tiempoAntelacionReserva = tiempoAntelacionReserva;
            _fechaBaja = fechaBaja;
            _motivoBaja = motivoBaja;
            _directorDelCi = directorDelCi;
            _cientificos = cientificos;
            _recursosTecnologicos = recursosTecnologicos;
        }
    }
}
