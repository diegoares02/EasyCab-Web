using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TallerFinal.Models
{
    public class Persona
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PersonaId { get; set; }
        public int CI { get; set; }
        public string Procedencia { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Celular { get; set; }
        public string Tipo { get; set; }
        public int? SucursalId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual Licencia_Conducir Licencia_Conducir { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}