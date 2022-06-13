namespace BlazorApp.TechResourceManagement.Domain
{
    public class AsignacionCientificoDelCI
    {
        private DateTime _fechaHoraDesde { get; set; }
        private DateTime? _fechaHoraHasta { get; set; }
        private PersonalCientifico _personalCientifico { get; set; }
        private IList<Turno> _turnos { get; set; }
        public AsignacionCientificoDelCI(DateTime fechaHoraDesde, DateTime? fechaHoraHasta,
            PersonalCientifico personalCientifico, IList<Turno> turnos)
        {
            _fechaHoraDesde = fechaHoraDesde;
            _fechaHoraHasta = fechaHoraHasta;
            _personalCientifico = personalCientifico;
            _turnos = turnos;
        }

    }
}
