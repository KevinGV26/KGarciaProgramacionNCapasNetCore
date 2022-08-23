using Microsoft.AspNetCore.Mvc;
//using Microsoft.Xrm.Sdk.Messages;
using System.IdentityModel.Tokens.Jwt;


namespace PL.Controllers
{
    public class VentaProducto : Controller
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

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {
            producto.Nombre = (producto.Nombre == null) ? "" : producto.Nombre;
            //materia.Semestre.IdSemestre = (materia.Semestre.IdSemestre == null) ? "" : empleado.ApellidoPaterno;
            //producto.Departamento.Area = producto.Departamento.Area == 0 ? 0 : producto.Departamento.Area;

            ML.Result result = BL.Producto.GetAll(producto);
            producto.Productos = result.Objects;

            //ML.Result resultSemestre = BL.Semestre.GetAll();
            //materia.Semestre.Semestres = resultSemestre.Objects;


            return View(producto);
        }



        public ActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CardPost(ML.Producto producto)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();

            ventaProducto.VentaProductos = new List<object>();

            if(HttpContext.Session.GetString("Producto")==null)
            {
                producto.PrecioUnitario = producto.PrecioUnitario = 1;

                ventaProducto.VentaProductos.Add(producto);

                HttpContext.Session.SetString("Prodcuto",Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));   

                var session=HttpContext.Session.GetString("Producto");
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                foreach(var obj in ventaSession)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());

                    ventaProducto.VentaProductos.Add(objProducto);
                }
                foreach(ML.Producto venta in ventaProducto.VentaProductos.ToList())
                {
                    if(producto.IdProducto == venta.IdProducto)
                    {
                        venta.PrecioUnitario=producto.PrecioUnitario+1;   
                    }
                    else
                    {
                        producto.PrecioUnitario=venta.PrecioUnitario=1;


                        ventaProducto.VentaProductos.Add(producto);
                    }
                }
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
            }
            if(HttpContext.Session.GetString("Producto")!=null)
            {
                ViewBag.Message = "El producto se ha agregado correctamente al carrito";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al agregar el producto";
                return PartialView("Modal");
            }
        }



        [HttpGet]
        public ActionResult ResultCompra(ML.VentaProducto ventaProducto)
        {

            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();

            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                ventaProducto.VentaProductos = new List<object>();

                foreach(var obj in ventaSession)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());

                    ventaProducto.VentaProductos.Add(objProducto);
                }
            }

            return View(ventaProducto);
        }

    }
}
