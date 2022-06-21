namespace BlazorApp.TechResourceManagement.Domain
{
    public class Turno
    {
        //Variables
        private DateTime fechaGeneracion { get; set; }
        private DayOfWeek diaSemana { get; set; }
        private DateTime fechaHoraInicio { get; set; }
        private DateTime fechaHoraFin { get; set; }
        private IList<CambioEstadoTurno> cambioEstadoTurno { get; set; }
        public string Text = "Turno";
        //Getter
        public DateTime FechaHoraInicio { get => fechaHoraInicio; }
        public DateTime? FechaHoraFin { get => fechaHoraFin; }
        //Constructor
        public Turno(DateTime fechaGeneracion, DayOfWeek diaSemana, DateTime fechaHoraInicio, DateTime fechaHoraFin, IList<CambioEstadoTurno> cambioEstadoTurno)
        {
            this.fechaGeneracion = fechaGeneracion;
            this.diaSemana = diaSemana;
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = fechaHoraFin;
            this.cambioEstadoTurno = cambioEstadoTurno;
        }
        //Metodos
        public Turno MostrarTurno() => this;
        public string MostrarEstadoActual() => cambioEstadoTurno.First(e => e.EsActualCET()).MostrarEstadoActual().MostrarEstado();

        public bool EsPosteriorAlDiaDeHoy(DateTime dateTime)
        {
            return fechaHoraInicio.Date >= dateTime.Date;
        }
        public bool EstoyDisponible()
        {
            return cambioEstadoTurno.Any(e => e.EsActualCET());
        }
    }
}
