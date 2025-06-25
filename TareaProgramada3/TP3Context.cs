using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TareaProgramada3
{
    public class TP3Context : DbContext
    {
        public TP3Context() : base("TP3Connection") { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Numero> Numeros { get; set; }
    }
}
