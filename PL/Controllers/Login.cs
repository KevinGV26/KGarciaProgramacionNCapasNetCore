using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Login : Controller
    {
        
        public ActionResult login()
        {
            return View(login);
        }
    }
}
