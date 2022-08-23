using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Producto : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            //instanciamos la clase producto
            ML.Producto producto = new ML.Producto();

            //asignamos la capa con el metodo a la clase result

            producto.Departamento = new ML.Departamento();

            producto.Departamento.Area = new ML.Area();

            producto.Departamento.IdDepartamento = (producto.Departamento.IdDepartamento == 0) ? 0 : producto.Departamento.IdDepartamento;


            ML.Result result = BL.Producto.GetAll(producto);

            ML.Result ResultArea = BL.Area.GetAll();


            if (result.Correct && ResultArea.Correct)
            {
                producto.Productos = result.Objects;

                producto.Departamento=new ML.Departamento();
                producto.Departamento.Area = new ML.Area(); 
                producto.Departamento.Area.Areas = ResultArea.Objects;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Algo fallo";
            }

            return View(producto);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {

            producto.Departamento = new ML.Departamento();   

            producto.Departamento.Area=new ML.Area();   

            producto.Departamento.IdDepartamento = (producto.Departamento.IdDepartamento == 0) ? 0 : producto.Departamento.IdDepartamento;

            //asignamos la capa con el metodo a la clase result

            ML.Result result = BL.Producto.GetAll(producto);

            ML.Result ResultArea = BL.Area.GetAll();


            if (result.Correct && ResultArea.Correct)
            {
                producto.Productos = result.Objects;

                producto.Departamento = new ML.Departamento();
                producto.Departamento.Area = new ML.Area();
                producto.Departamento.Area.Areas = ResultArea.Objects;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Algo fallo";
            }

            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        { 
            // instanciamos el modelo
            ML.Producto producto = new ML.Producto();
            //instanciamo el rol


            producto.Proveedor = new ML.Proveedor();
            producto.Departamento.Area = new ML.Area();

            ML.Result resultproveedor = BL.Proveedor.GetAll();
            ML.Result resultarea = BL.Area.GetAll();

            if (resultproveedor.Correct && resultarea.Correct)
            {
                if (IdProducto == null)
                {//Add

                    producto.Proveedor = new ML.Proveedor();
                    producto.Proveedor.Proveedores = resultproveedor.Objects;

                    producto.Departamento.Area = new ML.Area();
                    producto.Departamento.Area.Areas = resultarea.Objects;
                    return View(producto);
                }
                else //Update
                {
                    ML.Result result = BL.Producto.GetById(IdProducto.Value);
                    if (result.Correct)
                    {
                        producto = ((ML.Producto)result.Object);

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.Proveedores = resultproveedor.Objects;


                        producto.Departamento.Area = new ML.Area();

                        producto.Departamento.Area.Areas = resultarea.Objects;

                        return View(producto);

                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            return View("Modal");

        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            IFormFile imagen = Request.Form.Files["fuImage"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                producto.Imagen = Convert.ToBase64String(ImagenByte);

            }

            if (producto.IdProducto == 0)
            {//add
                //instanciamos el metodo result y asignamos la capa del metodo agregar
                ML.Result result = BL.Producto.Add(producto);

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
                ML.Result result = BL.Producto.Update(producto);
            }
            return View("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            //instaciamos el modelo
            ML.Producto producto = new ML.Producto();

            //Indica que sera solo por medio del IdUsuario
            producto.IdProducto = IdProducto;

            //Mandamos llamer  el metodo
            ML.Result result = BL.Producto.Delete(producto);

            //Validacion
            if (result.Correct)
            {
                ViewBag.Mensaje = "Eliminado Correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar";
            }
            //Regresamos la vista del modal
            return View("Modal");
        }


        public JsonResult DepartamentoGetByIdArea(int IdArea)
        {
            ML.Result result = BL.Departamento.DepartamentoGetByIdArea(IdArea);

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
