namespace BlazorApp.TechResourceManagement.Domain
{
    public class Usuario
    {
        //Variables
        private string _usuario { get; set; }
        private string _clave { get; set; }
        private bool _habilitado { get; set; }
        public string UsuarioNombre { get => _usuario; }
        public string Clave { get => _clave; }
        public bool Habilitado { get => _habilitado; }
        //Constructor
        public Usuario(string usuario, string clave)
        {
            _usuario = usuario;
            _clave = clave;
            Habilitar();
        }
        //Metodos
        public void Habilitar()
        {
            _habilitado = true;
        }
        public void Inhabilitar()
        {
            _habilitado = !_habilitado;
        }
        public void ModificarPassword(string clave)
        {
            _clave = clave;
        }
    }
}
