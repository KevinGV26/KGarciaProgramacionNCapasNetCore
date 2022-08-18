using Microsoft.AspNetCore.Mvc;

namespace SL_WebAPI.Controllers
{

    [ApiController]
    public class Usuario : Controller
    {

        [HttpPost]
        [Route("api/usuario/add")]
        public IActionResult Add([FromBody]ML.Usuario usuario)
        {
            var result=BL.Usuario.AddUsuarioEF(usuario);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/usuario/delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {

            ML.Usuario usuario=new ML.Usuario();

            usuario.IdUsuario = IdUsuario;
            var result = BL.Usuario.DeleteUsuarioEF(usuario);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        [Route("api/Usuario/Update")]
        public IActionResult update([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.UpdateUsuarioEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [Route("/api/Usuario/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            var result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [Route("api/Usuario/GetById/{IdUsuario}")]
        [HttpGet]
        public IActionResult GetById(int IdUsuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            var result = BL.Usuario.GetByIdUsuario(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
