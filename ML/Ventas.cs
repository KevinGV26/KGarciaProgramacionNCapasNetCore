using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Ventas
    {
        public int IdVenta { set; get; }

        public ML.Usuario? usuario { set; get; }

        public decimal Decimal { set; get; }    

        public ML.MetodoPago? metodoPago { set; get; }

        public string fecha { set; get; }

    }
}
