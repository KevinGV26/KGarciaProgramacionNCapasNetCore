using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class VentaProducto
    {
        public static void GetAllCarrito()
        {

            ML.Result result = new ML.Result();

            try
            {
                using(DL.KGarciaProgramacionNCapasContext context=new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.VentaProductos.FromSqlRaw($"VentaProductoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.VentaProducto ventaProducto= new ML.VentaProducto();

                            ventaProducto.IdVentaProducto= obj.IdVentaProducto;

                            //instanciamos las propiedades de navegacion
                            ventaProducto.ventas = new ML.Ventas();
                            ventaProducto.ventas.IdVenta = obj.IdVentas.Value;

                            ventaProducto.Cantidad= obj.Cantidad;


                            //instanciamos las propiedades de navegacion
                            ventaProducto.producto = new ML.Producto();

                            ventaProducto.producto.IdProducto = obj.IdProducto.Value;
                            //guardarlos en la lista
                            result.Objects.Add(ventaProducto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                        result.ErrorMessage = "Error al mostrar la venta de los productos";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
        }
    }
}
