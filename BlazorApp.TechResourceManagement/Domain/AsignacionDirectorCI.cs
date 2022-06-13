namespace BlazorApp.TechResourceManagement.Domain
{
    public class AsignacionDirectorCI
    {
        private DateTime _fechaHoraDesde { get; set; }
        private DateTime? _fechaHoraHasta { get; set; }
        private PersonalCientifico _personalCientifico { get; set; }
        public AsignacionDirectorCI(DateTime fechaHoraDesde, DateTime? fechaHoraHasta, PersonalCientifico personalCientifico)
        {
            _fechaHoraDesde = fechaHoraDesde;
            _fechaHoraHasta = fechaHoraHasta;
            _personalCientifico = personalCientifico;
        }
    }
}
