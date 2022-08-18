using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Estado
    {
        public int IdEstado { get; set; }

        public string? Nombre { get; set; }
        public ML.Pais? Pais { get; set; }
        public List<object>? Estados { get; set; }
    }
}
