namespace BlazorApp.TechResourceManagement.Domain
{
    public class CambioEstadoRT
    {
        private DateTime fechaHoraDesde { get; set; }
        private DateTime? fechaHoraHasta { get; set; }
        private Estado estado { get; set; }
        public CambioEstadoRT(DateTime fechaHoraDesde, DateTime? fechaHoraHasta, Estado estado)
        {
            this.fechaHoraDesde = fechaHoraDesde;
            this.fechaHoraHasta = fechaHoraHasta;
            this.estado = estado;
        }
        public bool EsEstadoActual()
        {
            return !fechaHoraHasta.HasValue;
        }
        public Estado MostrarEstado() => estado;
        public bool EsBaja()
        {
            return estado.EsBajaTecnica() || estado.EsBajaDefinitiva();
        }
    }
}
