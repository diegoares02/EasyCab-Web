using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public int Cod { get; set; }
        public string Zona { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}