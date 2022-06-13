namespace BlazorApp.TechResourceManagement.Domain
{
    public class Estado
    {
        //Variables
        private string nombre { get; set; }
        private string descripcion { get; set; }
        private string ambito { get; set; }
        private bool esReservable { get; set; }
        private bool esCancelable { get; set; }
        //Constructor
        public Estado(string nombre, string descripcion, string ambito, bool esReservable, bool esCancelable)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.ambito = ambito;
            this.esReservable = esReservable;
            this.esCancelable = esCancelable;
        }
        //Metodos
        public string MostrarEstado() => $"{nombre} - {descripcion}";

    }
}