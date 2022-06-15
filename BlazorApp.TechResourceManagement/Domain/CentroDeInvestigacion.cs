namespace BlazorApp.TechResourceManagement.Domain
{
    public class CentroDeInvestigacion
    {
        private string nombre { get; set; }
        private string sigla { get; set; }
        private string direccion { get; set; }
        private string edificio { get; set; }
        private int piso { get; set; }
        private string coordenadas { get; set; }
        private IList<string> telefonosContacto { get; set; }
        private string correoElectronico { get; set; }
        private string numeroResolucionCreacion { get; set; }
        private DateTime fechaResolucionCreacion { get; set; }
        private string reglamento { get; set; }
        private IList<string> caracteristicasGenerales { get; set; }
        private DateTime fechaAlta { get; set; } = DateTime.Now;
        private TimeSpan tiempoAntelacionReserva { get; set; }
        private DateTime? fechaBaja { get; set; }
        private string motivoBaja { get; set; }
        private IList<AsignacionDirectorCI> directorDelCi { get; set; }
        private IList<AsignacionCientificoDelCI> cientificos { get; set; }
        private IList<RecursoTecnologico> recursosTecnologicos { get; set; }

        public string Nombre { get => nombre; }
        public string Sigla { get => sigla; }

        public CentroDeInvestigacion(string nombre, string sigla, string direccion, string edificio, int piso,
            string coordenadas, IList<string> telefonosContacto, string correoElectronico,
            string numeroResolucionCreacion, DateTime fechaResolucionCreacion,
            string reglamento, IList<string> caracteristicasGenerales,
            DateTime fechaAlta, TimeSpan tiempoAntelacionReserva,
            DateTime? fechaBaja, string motivoBaja,
            IList<AsignacionDirectorCI> directorDelCi,
            IList<AsignacionCientificoDelCI> cientificos,
            IList<RecursoTecnologico> recursosTecnologicos)
        {
            this.nombre = nombre;
            this.sigla = sigla;
            this.direccion = direccion;
            this.edificio = edificio;
            this.piso = piso;
            this.coordenadas = coordenadas;
            this.telefonosContacto = telefonosContacto;
            this.correoElectronico = correoElectronico;
            this.numeroResolucionCreacion = numeroResolucionCreacion;
            this.fechaResolucionCreacion = fechaResolucionCreacion;
            this.reglamento = reglamento;
            this.caracteristicasGenerales = caracteristicasGenerales;
            this.fechaAlta = fechaAlta;
            this.tiempoAntelacionReserva = tiempoAntelacionReserva;
            this.fechaBaja = fechaBaja;
            this.motivoBaja = motivoBaja;
            this.directorDelCi = directorDelCi;
            this.cientificos = cientificos;
            this.recursosTecnologicos = recursosTecnologicos;
        }

        //Metodos
        public CentroDeInvestigacion MostrarCI() => this;
        public bool EsCIActual(string sigla) => this.sigla == sigla;
        public IList<RecursoTecnologico> MisRecursosTecnologicos() => recursosTecnologicos;
        public IList<AsignacionCientificoDelCI> MisCientificos() => cientificos;
        public bool EsCientificoDelCI(PersonalCientifico personalCientifico)
        {
            return MisCientificos().Any(x => x.EsCientificoActual(personalCientifico));
        }
    }
}
