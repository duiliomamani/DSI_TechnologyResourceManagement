namespace BlazorApp.TechResourceManagement.Domain
{
    public class AsignacionCientificoDelCI
    {
        private DateTime fechaHoraDesde { get; set; }
        private DateTime? fechaHoraHasta { get; set; }
        private PersonalCientifico personalCientifico { get; set; }
        private IList<Turno> turnos { get; set; }
        public AsignacionCientificoDelCI(DateTime fechaHoraDesde, DateTime? fechaHoraHasta,
            PersonalCientifico personalCientifico, IList<Turno> turnos)
        {
            this.fechaHoraDesde = fechaHoraDesde;
            this.fechaHoraHasta = fechaHoraHasta;
            this.personalCientifico = personalCientifico;
            this.turnos = turnos;
        }

        public bool EsActual(PersonalCientifico personalCientifico)
        {
            return this.personalCientifico.EsActual(personalCientifico);
        }

    }
}
