using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Ubicacion
    {
        public int UbicacionId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime Fecha { get; set; }
        public string PersonaId { get; set; }
        public virtual Persona Persona { get; set; }        
    }
}