using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProducto : Controller
    {
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
        //LLamando getall productos

        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    //instanciamos la clase producto
        //    ML.Producto producto = new ML.Producto();

        //    //asignamos la capa con el metodo a la clase result
        //    ML.Result result = BL.Producto.GetAll(producto);

        //    if (result.Correct)
        //    {
        //        producto.Productos = result.Objects;
        //    }
        //    else
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = "Algo fallo";
        //    }

        //    return View(producto);
        //}
        [HttpPost]
        public ActionResult Form()
        {
            return View();
        }

    }
}
