
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.KGarciaProgramacionNCapasContext context=new DL.KGarciaProgramacionNCapasContext())
                {
                    var query = context.Areas.FromSqlRaw($"AreaGetAll").ToList();
                    result.Objects=new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Area Area = new ML.Area();

                            Area.IdArea = obj.IdArea;
                            Area.Nombre = obj.Nombre;

                            result.Objects.Add(Area);
                        }
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
                result.ErrorMessage=ex.Message;
            }
            return result;
        }
    }
}
