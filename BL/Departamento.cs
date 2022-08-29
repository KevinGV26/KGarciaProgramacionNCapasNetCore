using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Departamento
    {
        public static ML.Result DepartamentoAdd(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.KGarciaProgramacionNCapasContext context=new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoAdd '{departamento.Nombre}',{departamento.Area.IdArea}");

                    if(query>=1)
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
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result DepartamentoGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.KGarciaProgramacionNCapasContext context=new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetAll").ToList();

                    result.Objects = new List<object>();

                    if(query!= null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre=obj.Nombre;
                            departamento.Area=new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;

                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }


                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;   
            }
            return result;
        }

        public static ML.Result DepartamentoGetByIdArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.KGarciaProgramacionNCapasContext context = new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdArea}").AsEnumerable().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento =new ML.Departamento();

                            departamento.IdDepartamento =obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            result.Objects.Add(departamento);
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
