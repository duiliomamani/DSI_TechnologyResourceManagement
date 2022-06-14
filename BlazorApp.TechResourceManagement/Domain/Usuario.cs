namespace BlazorApp.TechResourceManagement.Domain
{
    public class Usuario
    {
        //Variables
        private string usuario { get; set; }
        private string clave { get; set; }
        private bool habilitado { get; set; }
        //Constructor
        public Usuario(string usuario, string clave)
        {
            this.usuario = usuario;
            this.clave = clave;
            Habilitar();
        }
        //Metodos
        public bool EsTuUsuario(Usuario usuarioActual)
        {
            return usuario == usuarioActual.usuario;
        }
        public void Habilitar()
        {
            habilitado = true;
        }
        public void Inhabilitar()
        {
            habilitado = !habilitado;
        }
        public void ModificarPassword(string clave)
        {
            this.clave = clave;
        }
    }
}
