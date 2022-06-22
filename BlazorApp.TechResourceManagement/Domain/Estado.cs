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

        //Getter
        public string Nombre { get => nombre; }
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
        public Estado MostrarEstado() => this;
        public bool EsAmbitoRT() => ambito == "RT";
        public bool EsAmbitoTurno() => ambito == "Turno";
        public bool EsDisponible() => EsAmbitoTurno() && nombre == "Disponible";
        public bool EsReservado() => EsAmbitoTurno() && nombre == "Reservado";
        public bool EsBajaTecnica() => EsAmbitoRT() && nombre == "BajaTecnica";
        public bool EsBajaDefinitiva() => EsAmbitoRT() && nombre == "BajaDefinitiva";

    }
}