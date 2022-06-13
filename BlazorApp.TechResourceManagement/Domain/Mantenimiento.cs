namespace BlazorApp.TechResourceManagement.Domain
{
    public class Mantenimiento
    {
        private DateTime _fechaInicio { get; set; }
        private DateTime? _fechaFin { get; set; }
        private DateTime _fechaInicioPrevista { get;set; }  
        private string _motivoMantenimiento { get; set; }  
        private IList<ExtensionMantenimiento> _extensionMantenimiento { get; set; }

        public Mantenimiento(DateTime fechaInicio, DateTime? fechaFin, DateTime fechaInicioPrevista, 
            string motivoMantenimiento, IList<ExtensionMantenimiento> extensionMantenimiento)
        {
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _fechaInicioPrevista = fechaInicioPrevista;
            _motivoMantenimiento = motivoMantenimiento;
            _extensionMantenimiento = extensionMantenimiento;
        }
    }
}
