namespace BlazorApp.TechResourceManagement.Domain
{
    public class AsignacionResponsableTecnicoRT
    {
        private DateTime _fechaDesde { get; set; }
        private DateTime _fechaHasta { get; set; }
        private IList<RecursoTecnologico> _recursos { get; set; }
        private PersonalCientifico _personalCientifico { get;set; }
        public AsignacionResponsableTecnicoRT(DateTime fechaDesde, DateTime fechaHasta, IList<RecursoTecnologico>  recursos,
            PersonalCientifico personalCientifico)
        {
            _fechaDesde = fechaDesde;
            _fechaHasta = fechaHasta;
            _recursos = recursos;
            _personalCientifico = personalCientifico;
        }

    }
}
