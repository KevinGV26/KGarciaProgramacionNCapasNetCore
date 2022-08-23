using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Ventum
    {
        public Ventum()
        {
            VentaProductos = new HashSet<VentaProducto>();
        }

        public int IdVenta { get; set; }
        public int? Idusuario { get; set; }
        public decimal Total { get; set; }
        public int? IdMetodopago { get; set; }
        public DateTime Fecha { get; set; }

        public virtual MetodoPago? IdMetodopagoNavigation { get; set; }
        public virtual Usuario? IdusuarioNavigation { get; set; }
        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
