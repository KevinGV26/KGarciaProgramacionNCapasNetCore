using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Usuario : Controller
    {
        // GET: Usuario
        [HttpGet]

        //Obtener todos los registros
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();


            ML.Result result = BL.Usuario.GetAll();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio  un error";

            }
            return View(usuario);
        }


        [HttpGet]
        //Los muestra precargado
        public ActionResult Form(int? IdUsuario)
        {
            // instanciamos el modelo
            ML.Usuario usuario = new ML.Usuario();


            //instanciamo el rol
            usuario.Rol = new ML.Rol();

            ML.Result resultrol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();


            if (resultrol.Correct && resultPais.Correct)
            {
                if (IdUsuario == null)
                {//Add

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.Roles = resultrol.Objects;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    return View(usuario);
                }
                else //Update
                {
                    ML.Result result = BL.Usuario.GetByIdUsuario(IdUsuario.Value);
                    if (result.Correct)
                    {
                        usuario = ((ML.Usuario)result.Object);
                        ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                        ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.Roles = resultrol.Objects;
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                        return View(usuario);
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al realizar la consulta" + resultrol.ErrorMessage;

            }
            return View("Modal");
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            if (usuario.IdUsuario == 0)
            {//add
                //instanciamos el metodo result y asignamos la capa del metodo agregar
                ML.Result result = BL.Usuario.AddUsuarioEF(usuario);
                //Validamos
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se inserto correctamente";
                }
                else
                {
                    result.Correct = false;
                    ViewBag.Mensaje = "error al insertar" + result.ErrorMessage;

                }
                return View("Modal");

            }
            else //Update
            {
                ML.Result result = BL.Usuario.UpdateUsuarioEF(usuario);

                usuario = (ML.Usuario)result.Object;
            }
            return View("Modal");
        }
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.EstadoGetByIdPais(IdPais);

            return Json(result.Objects);
        }
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }
    }
}
