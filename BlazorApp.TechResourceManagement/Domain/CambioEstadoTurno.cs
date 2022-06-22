namespace BlazorApp.TechResourceManagement.Domain
{
    public class CambioEstadoTurno
    {
        private DateTime fechaHoraDesde { get; set; }
        private DateTime? fechaHoraHasta { get; set; }
        private Estado estado { get; set; }
        public CambioEstadoTurno(DateTime fechaHoraDesde, DateTime? fechaHoraHasta, Estado estado)
        {
            this.fechaHoraDesde = fechaHoraDesde;
            this.fechaHoraHasta = fechaHoraHasta;
            this.estado = estado;
        }
        public Estado MostrarEstado() => estado;
        public bool EsActualCET()
        {
            return !fechaHoraHasta.HasValue;
        }
        public bool EsDisponible()
        {
            return estado.EsDisponible();
        }
    }
}
