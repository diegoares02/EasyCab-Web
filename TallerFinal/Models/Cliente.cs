using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public int Telefono { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}