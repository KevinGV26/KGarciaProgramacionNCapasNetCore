using System;
using System.Collections.Generic;

namespace DL
{
    public partial class MetodoPago
    {
        public MetodoPago()
        {
            Venta = new HashSet<Ventum>();
        }

        public int IdMetodopago { get; set; }
        public string Metodo { get; set; } = null!;

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
