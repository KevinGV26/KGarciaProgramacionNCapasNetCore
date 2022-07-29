using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { set; get; }
        public string Nombre { set; get; }

        public string ApellidoPaterno { set; get; }

        public string ApellidoMaterno { set; get; }

        public string Email { set; get; }

        //propiedad de navegación

        public ML.Rol Rol { set; get; }

        public string Password { set; get; }

        public string Sexo { set; get; }

        public string Telefono { set; get; }

        public string Celular { set; get; }

        public string FechaNacimiento { set; get; }

        public string Curp { set; get; }

        public byte[] Imagen { set; get; }

        public string UserName { set; get; }



        public ML.Direccion Direccion { get; set; }

        public List<object> Usuarios { set; get; }
    }
}
