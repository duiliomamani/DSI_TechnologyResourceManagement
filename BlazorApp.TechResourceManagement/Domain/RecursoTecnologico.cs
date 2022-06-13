﻿namespace BlazorApp.TechResourceManagement.Domain
{
    public class RecursoTecnologico
    {
        //Variables
        private long numeroRT { get; set; }
        private DateTime fechaAlta { get; set; } = DateTime.Now;
        private IList<string> imagenes { get; set; }
        private int periodicidadMantenimientoPrev { get; set; }
        private int duracionMantenimientoPrev { get; set; }
        private double fraccionarioHorarioTurnos { get; set; }
        private IList<CambioEstadoRT> cambioEstadoRT { get; set; }
        private TipoRecursoTecnologico tipoDelRT { get; set; }
        private Modelo modeloDelRT { get; set; }
        private IList<CaracteristicaRecurso> caracteristicasRecurso { get; set; }
        private IList<Turno> turnos { get; set; }
        private IList<HorarioRT> disponibilidad { get; set; }
        private IList<Mantenimiento> mantenimientos { get; set; }
        //Constructor
        public RecursoTecnologico(long numeroRT, DateTime fechaAlta, IList<string> imagenes, int periodicidadMantenimientoPrev, int duracionMantenimientoPrev, double fraccionarioHorarioTurnos, IList<CambioEstadoRT> cambioEstadoRT, TipoRecursoTecnologico tipoDelRT, Modelo modeloDelRT, IList<CaracteristicaRecurso> caracteristicasRecurso, IList<Turno> turnos, IList<HorarioRT> disponibilidad, IList<Mantenimiento> mantenimientos)
        {
            this.numeroRT = numeroRT;
            this.fechaAlta = fechaAlta;
            this.imagenes = imagenes;
            this.periodicidadMantenimientoPrev = periodicidadMantenimientoPrev;
            this.duracionMantenimientoPrev = duracionMantenimientoPrev;
            this.fraccionarioHorarioTurnos = fraccionarioHorarioTurnos;
            this.cambioEstadoRT = cambioEstadoRT;
            this.tipoDelRT = tipoDelRT;
            this.modeloDelRT = modeloDelRT;
            this.caracteristicasRecurso = caracteristicasRecurso;
            this.turnos = turnos;
            this.disponibilidad = disponibilidad;
            this.mantenimientos = mantenimientos;
        }

        //Metodos
    }
}
