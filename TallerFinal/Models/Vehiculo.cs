using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Vehiculo
    {
        [Key,ForeignKey("Persona")]
        public string PersonaId { get; set; }
        public string Nro_Placa { get; set; }
        public string Marca { get; set; }
        public int Modelo { get; set; }
        public string Dia_Restriccion { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}