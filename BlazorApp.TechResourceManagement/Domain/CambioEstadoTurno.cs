namespace BlazorApp.TechResourceManagement.Domain
{
    public class CambioEstadoTurno
    {
        private DateTime _fechaHoraDesde { get; set; }
        private DateTime? _fechaHoraHasta { get; set; }
        public CambioEstadoTurno(DateTime fechaHoraDesde, DateTime? fechaHoraHasta)
        {
            _fechaHoraDesde = fechaHoraDesde;
            _fechaHoraHasta = fechaHoraHasta;
        }
    }
}
