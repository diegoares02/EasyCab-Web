using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public int Tarifa { get; set; }
        public DateTime Fecha { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public int ClienteId { get; set; }
        [ForeignKey("Persona")]
        public string Operador { get; set; }
        public virtual Persona Persona { get; set; }
        [ForeignKey("Vehiculo")]
        public string Conductor { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}