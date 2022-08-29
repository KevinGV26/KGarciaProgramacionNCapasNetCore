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

        public ActionResult Form(int? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();

            if(IdDepartamento == null)
            {
                return View(departamento);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            if(departamento.IdDepartamento == 0)
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

            }
            return View("Modal");
        }
    }
}