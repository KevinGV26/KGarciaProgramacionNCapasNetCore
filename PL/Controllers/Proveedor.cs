using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Proveedor : Controller
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Proveedor proveedor = new ML.Proveedor();

            ML.Result result = BL.Proveedor.GetAll();


            if (result.Correct)
            {
                proveedor.Proveedores = result.Objects;
            }
            else
            {
                result.Correct = false;

                result.ErrorMessage = "Ocurrio un error";
            }
            return View(proveedor);
        }

        [HttpGet]
        public ActionResult Form(int? IdProveedor)
        { 
            // instanciamos el modelo
            ML.Proveedor proveedor= new ML.Proveedor();

            if(IdProveedor==null)
            {

                return View(proveedor);
            }
            else
            {
                ML.Result result = BL.Proveedor.ProveedorGetById(IdProveedor.Value);

                if(result.Correct)
                {
                    proveedor = ((ML.Proveedor)result.Object);

                    return View(proveedor);
                }
                else
                {
                    result.Correct = false;
                    ViewBag.Mensaje = "Ocurrio un error al realizar la consulta";

                }
            }
            return View("Modal");
        }

        [HttpPost]
        public ActionResult Form(ML.Proveedor proveedor)
        {
            if(proveedor.IdProveedor == 0)
            {
                ML.Result result=BL.Proveedor.ProveedorAdd(proveedor);

                if(result.Correct)
                {
                    ViewBag.Mensaje = "Se inserto correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "error al insertar" + result.ErrorMessage;
                }
                return View("Modal");
            }
            else
            {

                ML.Result result=BL.Proveedor.ProveedorUpdate(proveedor);

                proveedor = (ML.Proveedor)result.Object;
            }
            return View("Modal");

        }


        [HttpGet]
        public ActionResult Delete(int IdProveedor)
        {
            //instaciamos el modelo
            ML.Proveedor proveedor= new ML.Proveedor();

      
            proveedor.IdProveedor= IdProveedor;

            //Mandamos llamer  el metodo
            ML.Result result = BL.Proveedor.ProveedorDelete(proveedor);

            //Validacion
            if (result.Correct)
            {
                ViewBag.Mensaje = "Eliminado Correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar el proveedor";
            }
            //Regresamos la vista del modal
            return View("Modal");
        }
    }
}
