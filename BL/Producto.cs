using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}',{producto.PrecioUnitario},{producto.Stock},{producto.Proveedor.IdProveedor},{producto.Departamento.IdDepartamento},'{producto.Descripcion}','{producto.Imagen}'");

                    if (query > 0)
                    {
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

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.Nombre}',{producto.PrecioUnitario},{producto.Stock},'{producto.Proveedor.IdProveedor}','{producto.Departamento.IdDepartamento}','{producto.Descripcion}', '{producto.Imagen}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el registro";
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

        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new())
                {
                    var consulta = context.Database.ExecuteSqlRaw($"ProductoDelete {producto.IdProducto}");

                    if (consulta >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el registro";
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = obj.PrecioUnitario;
                            producto.Stock = obj.Stock;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;
                            //guardarlos en la lista
                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                        result.ErrorMessage = "erro al mostrar los productos";
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

        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();


                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;
                        //propiedad de navegacón
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;
                     
                        //Propiedad de navegación
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;
                        producto.Departamento.Nombre = query.NombreDepartamento;
                        //producto.Departamento = query.NombreDepartamento;
                        producto.Descripcion = query.Descripcion;
                        //.object porque solo queremos un dato
                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al mostrar el producto";
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
        public static ML.Result ConvertirExcelDataTable(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Hoja 1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableProducto = new DataTable();

                        da.Fill(tableProducto);

                        if (tableProducto.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach( DataRow row in tableProducto.Rows)
                            {
                                ML.Producto producto = new ML.Producto();
                                producto.Nombre = row[0].ToString();
                                producto.PrecioUnitario = decimal.Parse(row[1].ToString());
                                producto.Stock= int.Parse(row[2].ToString());

                                producto.Proveedor = new ML.Proveedor();
                                producto.Proveedor.IdProveedor = int.Parse(row[3].ToString());

                                producto.Departamento= new ML.Departamento();
                                producto.Departamento.IdDepartamento=int.Parse(row[4].ToString());

                                producto.Descripcion = row[5].ToString();

                                result.Objects.Add(producto);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableProducto;

                        if (tableProducto.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }

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

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects =new List<object>();

                int i = 1;


                foreach(ML.Producto producto in Objects)
                {
                    ML.ExcelErrores error = new ML.ExcelErrores();

                    error.IdRegistro = i;

                    if(producto.Nombre=="")
                    {
                        error.Message += "Ingrese el nombre";
                    }
                    if(producto.PrecioUnitario.ToString()=="")
                    {

                        error.Message += "Ingrese el Precio";

                    }

                    if (producto.Stock.ToString() == "")
                    {
                        error.Message += "Ingrese el stock";
                    }
                    if (producto.Proveedor.IdProveedor.ToString() == "")
                    {

                        error.Message += "Ingrese el proveedor";

                    }
                    if (producto.Departamento.IdDepartamento.ToString() == "")
                    {
                        error.Message += "Ingrese el departamento";
                    }
                    if (producto.Descripcion == "")
                    {

                        error.Message += "Ingrese la descripcion";

                    }

                    if (error.Message != null)
                    {
                        result.Objects.Add(error);
                    }

                    i++;
                }
            }
            catch(Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}