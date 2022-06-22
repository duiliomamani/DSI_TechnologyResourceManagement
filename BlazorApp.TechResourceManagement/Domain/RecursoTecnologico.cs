namespace BlazorApp.TechResourceManagement.Domain
{
    public class RecursoTecnologico
    {
        //Variables privadas
        private long numeroRT { get; set; }
        private DateTime fechaAlta { get; set; } = DateTime.Now;
        private IList<string> imagenes { get; set; }
        private int periodicidadMantenimientoPrev { get; set; }
        private int duracionMantenimientoPrev { get; set; }
        private double fraccionarioHorarioTurnos { get; set; }
        private IList<CambioEstadoRT> cambioEstadoRT { get; set; }
        private TipoRecursoTecnologico tipoDelRT { get; set; }
        private Modelo modeloDelRT { get; set; }
        private IList<CaracteristicaRecurso> caracteristicasRecurso { get; set; }
        private IList<Turno> turnos { get; set; }
        private IList<HorarioRT> disponibilidad { get; set; }
        private IList<Mantenimiento> mantenimientos { get; set; }

        //Getter
        public long NumeroRT { get => numeroRT; }
        public Modelo ModeloDelRT { get => modeloDelRT; }

        //Constructor
        public RecursoTecnologico(long numeroRT, DateTime fechaAlta, IList<string> imagenes, int periodicidadMantenimientoPrev, int duracionMantenimientoPrev, double fraccionarioHorarioTurnos, IList<CambioEstadoRT> cambioEstadoRT, TipoRecursoTecnologico tipoDelRT, Modelo modeloDelRT, IList<CaracteristicaRecurso> caracteristicasRecurso, IList<Turno> turnos, IList<HorarioRT> disponibilidad, IList<Mantenimiento> mantenimientos)
        {
            this.numeroRT = numeroRT;
            this.fechaAlta = fechaAlta;
            this.imagenes = imagenes;
            this.periodicidadMantenimientoPrev = periodicidadMantenimientoPrev;
            this.duracionMantenimientoPrev = duracionMantenimientoPrev;
            this.fraccionarioHorarioTurnos = fraccionarioHorarioTurnos;
            this.cambioEstadoRT = cambioEstadoRT;
            this.tipoDelRT = tipoDelRT;
            this.modeloDelRT = modeloDelRT;
            this.caracteristicasRecurso = caracteristicasRecurso;
            this.turnos = turnos;
            this.disponibilidad = disponibilidad;
            this.mantenimientos = mantenimientos;
        }

        //Metodos
        public Modelo MostrarModelo() => modeloDelRT;
        public IList<Turno> MostrarMisTurnos(DateTime dateTime) => turnos.Where(tur => tur.EsPosteriorAlDiaDeHoy(dateTime)).ToList();
        public bool EsRecursoActual(long numeroRT) => this.numeroRT == numeroRT;
        public bool EsTipoSeleccionado(TipoRecursoTecnologico tipoRecursoTecnologico)
        {
            //Verifica que es del tipo seleccionado
            return tipoDelRT.EsTipoSeleccionado(tipoRecursoTecnologico);
        }
        public bool EstaDadoBaja()
        {
            //Verifico que el estado es dado de Baja
            return cambioEstadoRT.Any(x => x.EsEstadoActual() && x.EsBaja());
        }

        public string GetEstadoActual() => cambioEstadoRT.First(e => e.EsEstadoActual()).MostrarEstado().MostrarEstado();

        public RecursoTecnologico MostrarRT() => this;
    }
}
