using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TallerFinal.DAL
{
    public class TallerFinalContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TallerFinalContext() : base("name=TallerFinalContext")
        {
        }

        public System.Data.Entity.DbSet<TallerFinal.Models.Persona> Personas { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Sucursal> Sucursals { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Empresa> Empresas { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Licencia_Conducir> Licencia_Conducir { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Vehiculo> Vehiculoes { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Ubicacion> Ubicacions { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Area> Areas { get; set; }

        public System.Data.Entity.DbSet<TallerFinal.Models.Servicio> Servicios { get; set; }
    
    }
}
