namespace BlazorApp.TechResourceManagement.Domain
{
    public class ExtensionMantenimiento
    {
        private DateTime _fecha { get; set; }
        private DateTime _fechaFinPrevista { get; set; }    
        private string _motivo { get; set; }
        public ExtensionMantenimiento(DateTime fecha, DateTime fechaFinPrevista, string motivo)
        {
            _fecha = fecha;
            _fechaFinPrevista = fechaFinPrevista;
            _motivo = motivo;
        }
    }
}
