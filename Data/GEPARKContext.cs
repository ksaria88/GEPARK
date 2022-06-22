using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GEPARK.Models;

namespace GEPARK.Data
{
    public class GEPARKContext : DbContext
    {
        public GEPARKContext (DbContextOptions<GEPARKContext> options)
            : base(options)
        {
        }

        public DbSet<GEPARK.Models.Parking>? Parking { get; set; }

        public DbSet<GEPARK.Models.Tarifa>? Tarifa { get; set; }

        public DbSet<GEPARK.Models.TicketPerdido>? TicketPerdido { get; set; }

        public DbSet<GEPARK.Models.Usuario>? Usuario { get; set; }
        public DbSet<GEPARK.Models.ValidarTicket>? ValidarTicket { get; set; }
    }
}
