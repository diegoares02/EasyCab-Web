using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Usuario
    {
        [Key,ForeignKey("Persona")]
        public string PersonaId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual Persona Persona { get; set; }
    }
}