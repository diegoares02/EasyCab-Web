using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Sucursal
    {
        public int SucursalId { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}