using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        //agregar textbox de password y email
        public int IdUsuario { set; get; }

        [Required(ErrorMessage ="El  nombre es obligatorio")]
        //[RegularExpression(@"a-zA-Z '",ErrorMessage =("Solo letras"))]
        public string Nombre { set; get; }


        [Required (ErrorMessage ="El apellido paterno obligatorio")]
        [Display(Name ="Apellido Paterno")]
        public string ApellidoPaterno { set; get; }




        [Display(Name ="Apellido Materno")]
        public string ApellidoMaterno { set; get; }




        [Required(ErrorMessage ="El email es requerido")]
        public string Email { set; get; }

        //propiedad de navegación




        [Required(ErrorMessage ="El rol es requerido")]
        public ML.Rol Rol { set; get; }





        [Required(ErrorMessage ="El password es requerido")]
        public string Password { set; get; }





        [Required(ErrorMessage ="Es sexo requerido")]
        public string Sexo { set; get; }



        [Required(ErrorMessage = "El télefono es requerido")]

        //[RegularExpression(@"\A[0-9]{10}\z")]
        public string Telefono { set; get; }




        //[RegularExpression(@"\A[0-9]{10}\z",ErrorMessage =("solo acepta numeros"))]
        public string Celular { set; get; }





        //[Required(ErrorMessage ="La Fecha de nacimiento es requerida")]
        [Display(Name ="Fecha de nacimiento")]
        public string FechaNacimiento { set; get; }



        //[StringLength(18, ErrorMessage = "La longitud del curp no es válida.")]
        public string Curp { set; get; }

        public string Imagen { set; get; }

        public string UserName { set; get; }

        public bool Status { set; get; }



        public ML.Direccion? Direccion { get; set; }

        public List<object>? Usuarios { set; get; }



    }
}
