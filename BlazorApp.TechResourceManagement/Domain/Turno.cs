namespace BlazorApp.TechResourceManagement.Domain
{
    public class Turno
    {
        //Variables
        private DateTime _fechaGeneracion { get; set; }
        private DayOfWeek _diaSemana { get; set; }
        private DateTime _fechaHoraInicio { get; set; }
        private DateTime? _fechaHoraFin { get; set; }
        private IList<CambioEstadoTurno> _cambioEstadoTurnos { get; set; }
        //Constructor
        public Turno() { }
        public Turno(DateTime fechaGeneracion, DayOfWeek diaSemana, DateTime fechaHoraInicio, DateTime? fechaHoraFin)
        {
            _fechaGeneracion = fechaGeneracion;
            _diaSemana = diaSemana;
            _fechaHoraInicio = fechaHoraInicio;
            _fechaHoraFin = fechaHoraFin;
        }
        //Metodos
        public string MostrarTurno()
        {
            return $"Turno {_fechaGeneracion} - Dia de Semana {_diaSemana}";
        }
        public bool EstoyDisponible()
        {
            return _cambioEstadoTurnos.Count == 1;
        }
    }
}
