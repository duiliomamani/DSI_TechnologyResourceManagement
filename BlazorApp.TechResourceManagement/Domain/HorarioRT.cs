namespace BlazorApp.TechResourceManagement.Domain
{
    public class HorarioRT
    {
        private DayOfWeek _diaSemana { get; set; }
        private TimeSpan _horaDesde {  get; set; }  
        private TimeSpan? _horaHasta { get; set; }
        private DateTime _vigenciaDesde { get; set; }
        private DateTime  _vigenciaHasta { get; set; }
        public HorarioRT(DayOfWeek diaSemana, TimeSpan horaDesde, TimeSpan? horaHasta,
            DateTime vigenciaDesde, DateTime vigenciaHasta)
        {
            _diaSemana = diaSemana;
            _horaDesde = horaDesde;
            _horaHasta = horaHasta;
            _vigenciaDesde = vigenciaDesde;
            _vigenciaHasta = vigenciaHasta;
        }
    }
}
