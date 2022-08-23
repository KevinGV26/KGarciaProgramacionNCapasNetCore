using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaProducto
    {

        public int IdVentaProducto { set; get; }

        public int Cantidad { set; get; }

        public ML.Producto? producto { get; set; }

        public ML.Ventas? ventas { set; get; }

        public List<object>? VentaProductos { set; get; }

    }
}
