using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Informedebasedatos.Models;

namespace Informedebasedatos.Data
{
    public class InformedebasedatosContext : DbContext
    {
        public InformedebasedatosContext (DbContextOptions<InformedebasedatosContext> options)
            : base(options)
        {
        }

        public DbSet<Informedebasedatos.Models.Categoria> Categoria { get; set; } = default!;

        public DbSet<Informedebasedatos.Models.Cliente>? Cliente { get; set; }

        public DbSet<Informedebasedatos.Models.Informe>? Informe { get; set; }

        public DbSet<Informedebasedatos.Models.Usuario>? Usuario { get; set; }
    }
}
