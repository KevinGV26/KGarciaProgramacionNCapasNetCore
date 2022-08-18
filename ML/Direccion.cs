using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Direccion
    {

        [Display(Name ="Direccion")]
        public int IdDireccion { get; set; }


        [Required(ErrorMessage ="La calle es requerida")]
        public string? Calle { get; set; }


        //Validacion de rango 
        [StringLength(10, MinimumLength = 2)]
        [Display (Name ="Número Interior")]
        public string? NumeroInterior { get; set; }



        //Validacion de rango 
        [StringLength(10, MinimumLength = 1)]
        [Required(ErrorMessage ="El número exterior es requerido")]
        [Display(Name ="Número Exterior")]
        public string? NumeroExterior { get; set; }

        public ML.Colonia? Colonia { get; set; }

        public ML.Usuario? Usuario { get; set; }
    }
}
