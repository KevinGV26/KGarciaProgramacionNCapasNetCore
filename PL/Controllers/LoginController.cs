using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Login(ML.Usuario usuarioLogin) //INicar sesión
        {
            ML.Result result = BL.Usuario.UserGetByName(usuarioLogin.UserName);
            if (result.Correct)
            {
                //unboxig
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuarioLogin.Password == usuario.Password)
                {
                    return RedirectToAction("Index", "Home");


                }
                else
                {
                    ViewBag.Message = "Datos incorrectos";

                    return View("Modal");
                }

            }
            else
            {
                ViewBag.Message = "El email no esta registrado";
                return View("Modal");

            }
        }
    }
}
