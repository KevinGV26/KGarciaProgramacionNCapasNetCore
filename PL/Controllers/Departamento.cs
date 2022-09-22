using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Departamento : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Departamento departamento = new ML.Departamento();
            ML.Result result = BL.Departamento.DepartamentoGetAll();

            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
            }
            else
            {
                result.Correct = false;

                result.ErrorMessage = "Ocurrio un error";
            }
            return View(departamento);


        }

        [HttpGet]

        [HttpGet]
        public ActionResult Form(int? IdDepartamento)
        {

            ML.Departamento departamento = new ML.Departamento();

            departamento.Area = new ML.Area();

            ML.Result resultArea = BL.Area.GetAll();


            if (resultArea.Correct)
            {
                if (IdDepartamento == null)
                {
                    departamento.Area = new ML.Area();
                    departamento.Area.Areas = resultArea.Objects;
                    return View(departamento);
                }
                else
                {
                    ML.Result result = BL.Departamento.DepartamentoGetById(IdDepartamento.Value);

                    if (result.Correct)
                    {
                        departamento = (ML.Departamento)result.Object;

                        return View(departamento);
                    }
                    else
                    {
                        result.Correct = false;

                        ViewBag.message = "algo fallo";
                    }
                }

            }
            return View("Modal");


        }


        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            if (departamento.IdDepartamento == 0)
            {
                ML.Result result = BL.Departamento.DepartamentoAdd(departamento);

                if (result.Correct)
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

                ML.Result result = BL.Departamento.DepartamentoUpdate(departamento);

                departamento = (ML.Departamento)result.Object;
            }
            return View("Modal");
        }
    }
}