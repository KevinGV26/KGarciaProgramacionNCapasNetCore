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
            ML.Result result = BL.Producto.GetAll();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "ALgo fallo";
            }

            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        { // instanciamos el modelo
            ML.Producto producto = new ML.Producto();
            //instanciamo el rol


            producto.Proveedor = new ML.Proveedor();
            //producto.Departamento = new ML.Departamento();

            ML.Result resultproveedor = BL.Proveedor.GetAll();
            ML.Result resultdepartamento = BL.Departamento.GetAll();
            if (resultproveedor.Correct && resultdepartamento.Correct)
            {
                if (IdProducto == null)
                {//Add

                    producto.Proveedor = new ML.Proveedor();
                    producto.Proveedor.Proveedores = resultproveedor.Objects;
                    producto.Departamento = new ML.Departamento();
                    producto.Departamento.Departamentos = resultdepartamento.Objects;
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

                        producto.Departamento = new ML.Departamento();

                        producto.Departamento.Departamentos = resultdepartamento.Objects;
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
    }
}
