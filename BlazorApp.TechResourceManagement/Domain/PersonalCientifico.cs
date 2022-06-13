namespace BlazorApp.TechResourceManagement.Domain
{
    public class PersonalCientifico
    {
        private string legajo { get; set; }
        private string nombre { get; set; }
        private string apellido { get; set; }
        private long numeroDocumento { get; set; }
        private string correoElectronicoInstitucional { get; set; }
        private string correoElectronicoPersonal { get; set; }
        private long telefonoCelular { get; set; }
        private Usuario usuario { get; set; }
        public PersonalCientifico(string legajo, string nombre, string apellido,
            long numeroDocumento, string correoElectronicoInstitucional, string correoElectronicoPersonal,
            long telefonoCelular, Usuario usuario)
        {
            this.legajo = legajo;
            this.nombre = nombre;
            this.apellido = apellido;
            this.numeroDocumento = numeroDocumento;
            this.correoElectronicoInstitucional = correoElectronicoInstitucional;
            this.correoElectronicoPersonal = correoElectronicoPersonal;
            this.telefonoCelular = telefonoCelular;
            this.usuario = usuario;
        }
    }
}
