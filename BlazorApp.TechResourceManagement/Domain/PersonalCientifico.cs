namespace BlazorApp.TechResourceManagement.Domain
{
    public class PersonalCientifico
    {
        private long _legajo { get; set; }
        private string _nombre { get; set; }
        private string _apellido { get; set; }
        private long _numeroDocumento { get; set; }
        private string _correoElectronicoInstitucional { get; set; }
        private string _correoElectronicoPersonal { get; set; }
        private long _telefonoCelular { get; set; }
        private Usuario _usuario { get; set; }
        public PersonalCientifico(long legajo, string nombre, string apellido,
            long numeroDocumento, string correoElectronicoInstitucional, string correoElectronicoPersonal,
            long telefonoCelular, Usuario usuario)
        {
            _legajo = legajo;
            _nombre = nombre;
            _apellido = apellido;
            _numeroDocumento = numeroDocumento;
            _correoElectronicoInstitucional = correoElectronicoInstitucional;
            _correoElectronicoPersonal = correoElectronicoPersonal;
            _telefonoCelular = telefonoCelular;
            _usuario = usuario;
        }
    }
}
