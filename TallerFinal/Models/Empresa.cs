using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Empresa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmpresaId { get; set; }
        public string Nombre { get; set; }
        public int NIT { get; set; }
        public string PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }
}