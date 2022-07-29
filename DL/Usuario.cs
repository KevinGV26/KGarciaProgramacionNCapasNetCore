using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Direccions = new HashSet<Direccion>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; }
        public int? IdRol { get; set; }
        public string? Password { get; set; }
        public string? Sexo { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Curp { get; set; }
        public byte[]? Imagen { get; set; }
        public string? UserName { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }


        //ALias
        public string NombreRol { set; get; }


        //propiedades

        public int IdDireccion { set; get; }
        public string Calle { set; get; }
        public string NumeroInterior { set; get; }
        public string NumeroExterior { set; get; }
        public int IdColonia { set; get; }
        public string NombreColonia { set; get; }
        public int IdMunicipio { set; get; }
        public string NombreMunicipio { set; get; }
        public int IdEstado { set; get; }
        public string NombreEstado { set; get; }

        public int IdPais { set; get; }


        public string NombrePais { set; get; }
    }
}
