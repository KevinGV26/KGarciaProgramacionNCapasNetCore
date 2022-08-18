using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class Usuario : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public Usuario(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: Usuario
        [HttpGet]
        //Obtener todos los registros
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;

            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;


            ML.Result resultApi = new ML.Result();

            using(var client =new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebApi"]);

                var responseTask = client.GetAsync("api/Usuario/GetAll");

                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<ML.Result>();

                    readtask.Wait();

                    resultApi.Objects = new List<object>();

                    foreach(var resultItem in readtask.Result.Objects)
                    {
                        ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultApi.Objects.Add(resultUsuario);
                    }
                }
            }

            usuario.Usuarios = resultApi.Objects;

            ////ML.Result result = BL.Usuario.GetAll(usuario);

            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //}
            //else
            //{
            //    result.Correct = false;
            //    result.ErrorMessage = "Ocurrio  un error";

            //}
            return View(usuario);
        }

        [HttpPost]

        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;

            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;


            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                result.Correct = false;
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
                    ML.Result resultapibyid = new ML.Result();
                    using(var client=new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);

                        var responseTask = client.GetAsync("api/Usuario/GetById/"+ IdUsuario );

                        responseTask.Wait();    

                        var result=responseTask.Result;

                        if(result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();

                            readTask.Wait();

                            resultapibyid.Object = new List<object>();

                            var resultitem = readTask.Result.Object;

                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultitem.ToString());

                            resultapibyid.Object = resultUsuario;
                        }
                    }


                    //ML.Result result = BL.Usuario.GetByIdUsuario(IdUsuario.Value);
                    //if (result.Correct)
                    //{
                    usuario = ((ML.Usuario)resultapibyid.Object);
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
                    //}
                    //else
                    //{
                    //    result.Correct = false;
                    //}
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

            if(ModelState.IsValid)
            {
                IFormFile imagen = Request.Form.Files["fuImage"];
                if (imagen != null)
                {
                    byte[] ImagenByte = ConvertToBytes(imagen);
                    usuario.Imagen = Convert.ToBase64String(ImagenByte);

                }

                if (usuario.IdUsuario == 0)
                {//add

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);
                        //usuario.Usuarios = new List<object>();

                        var posTask = client.PostAsJsonAsync<ML.Usuario>("/api/usuario/add",usuario);

                        posTask.Wait();

                        var resultService = posTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se inserto correctamente";
                        }
                        else
                        {
                            ViewBag.Mensaje = "error al insertar";
                        }
                    }

                    //usuario.Usuarios = resultApi.Objects;
                    //instanciamos el metodo result y asignamos la capa del metodo agregar
                    //ML.Result result = BL.Usuario.AddUsuarioEF(usuario);
                    //Validamos
                    //if (result.Correct)
                    //{
                    //    ViewBag.Mensaje = "Se inserto correctamente";
                    //}
                    //else
                    //{
                    //    result.Correct = false;
                    //    ViewBag.Mensaje = "error al insertar" + result.ErrorMessage;

                    //}
                    return View("Modal");

                }
                else //Update
                {
                    ML.Result resultApiUpdate = new ML.Result();
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);

                        var posTask = client.PostAsJsonAsync<ML.Usuario>("api/usuario/update", usuario);

                        posTask.Wait(); 

                        var result=posTask.Result;

                        if(result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se actualizo correctamente";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error al actualizar";
                        }

                    }
                    //ML.Result result = BL.Usuario.UpdateUsuarioEF(usuario);

                    usuario = (ML.Usuario)resultApiUpdate.Object;
                }
                return View("Modal");

            }
            else
            {
                ML.Result resultrol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();

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
            
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            //instaciamos el modelo
            ML.Usuario usuario = new ML.Usuario();

            //Indica que sera solo por medio del IdUsuario
            usuario.IdUsuario = IdUsuario;

            //Mandamos llamer  el metodo
            //ML.Result result = BL.Usuario.DeleteUsuarioEF(usuario);


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebApi"]);

                var response = client.GetAsync("api/usuario/delete/" + IdUsuario);

                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Usuario eliminado";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo  eliminar el usuario";
                }
            }
            //Validacion
            //if (result.Correct)
            //{
            //    ViewBag.Mensaje = "Eliminado Correctamente";
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Ocurrio un error al eliminar";
            //}
            //Regresamos la vista del modal
            return View("Modal");
        }

        [HttpGet]
        public ActionResult UpdateStatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetByIdUsuario(IdUsuario);

            if (result.Correct)
            {
                usuario = (ML.Usuario)result.Object;

                //uso del iperador ternario
                usuario.Status = (usuario.Status) ? false : true;
                
                ML.Result resultUpdate = BL.Usuario.UpdateUsuarioEF(usuario);

                if(resultUpdate.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo el status";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actaulizar el status";
                }
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

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
