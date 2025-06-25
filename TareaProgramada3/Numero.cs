using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaProgramada3
{
    public class Numero
    {
        public int NumeroId { get; set; }
        public int Orden { get; set; }
        public int Num { get; set; }

        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
