namespace BlazorApp.TechResourceManagement.Domain
{
    public class AsignacionDirectorCI
    {
        private DateTime fechaHoraDesde { get; set; }
        private DateTime? fechaHoraHasta { get; set; }
        private PersonalCientifico personalCientifico { get; set; }
        public AsignacionDirectorCI(DateTime fechaHoraDesde, DateTime? fechaHoraHasta, PersonalCientifico personalCientifico)
        {
            this.fechaHoraDesde = fechaHoraDesde;
            this.fechaHoraHasta = fechaHoraHasta;
            this.personalCientifico = personalCientifico;
        }
    }
}
