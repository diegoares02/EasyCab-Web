using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Licencia_Conducir
    {
        [Key,ForeignKey("Persona")]
        public string PersonaId { get; set; }
        public string Categoria { get; set; }
        public DateTime Fecha_Expiracion { get; set; }
        public virtual Persona Persona { get; set; }
    }
}