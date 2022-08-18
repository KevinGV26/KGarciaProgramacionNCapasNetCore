using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BL
{
    public class Proveedor
    {

        public static ML.Result ProveedorAdd(ML.Proveedor proveedor)
        {
            ML.Result result= new ML.Result();
            try
            {
                using(DL.KGarciaProgramacionNCapasContext context=new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorAdd '{proveedor.Telefono}','{proveedor.Nombre}'");

                    //La condicion siemrpe debe ser >=1
                    if(query>=1)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct=false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
            }

            return result;
        }
        public static ML.Result ProveedorDelete(ML.Proveedor proveedor)
        {
            ML.Result result= new ML.Result();
            try
            {
                using(DL.KGarciaProgramacionNCapasContext context= new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorDelete {proveedor.IdProveedor}");

                    if(query>=1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el proveedor";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
            }
            return result;
        }

        public static ML.Result ProveedorUpdate(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorUpdate {proveedor.IdProveedor},'{proveedor.Telefono}','{proveedor.Nombre}'");

                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
            }
            return result;
        }

        public static ML.Result ProveedorGetById(int IdProveedor)
        {
            ML.Result result= new ML.Result();
            try
            {
                using(DL.KGarciaProgramacionNCapasContext context =new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Proveedors.FromSqlRaw($"ProveedorGetById {IdProveedor}").AsEnumerable().FirstOrDefault();


                    result.Objects = new List<object>();

                    if(query!=null)
                    {
                        ML.Proveedor proveedor = new ML.Proveedor();

                        proveedor.IdProveedor = query.IdProveedor;
                        proveedor.Telefono = query.Telefono;
                        proveedor.Nombre=query.Nombre;

                        result.Object = proveedor;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                        result.ErrorMessage = "Error para mostrar proveedor";
                    }

                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Proveedors.FromSqlRaw($"ProveedorGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = obj.IdProveedor;
                            proveedor.Telefono = obj.Telefono;
                            proveedor.Nombre = obj.Nombre;

                            result.Objects.Add(proveedor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
