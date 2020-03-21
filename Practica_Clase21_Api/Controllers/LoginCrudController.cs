using System.Threading.Tasks;
using Common.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Practica_Clase21_Api.Controllers
{
  /*
   * Implementar: (Inyeccion de dependencia)
   * - Logger
   * - Servicios a utilizar
   * 
   * **/

  [Route("api/LoginCrud")]
  [ApiController]
  [EnableCors("_myPolicy_")]
  public class LoginCrudController : ControllerBase
  {
    private ILogger<LoginCrudController> _logger;
    private ILoginCrudService _loginCrudService;

    public LoginCrudController(ILogger<LoginCrudController> logger, ILoginCrudService loginCrudService)
    {
      _logger = logger;
      _loginCrudService = loginCrudService;

      _logger.LogInformation("Contructor LoginCrudController");
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] UserDTO user)
    {
      //No verifico si los datos enviados son nullos
      //Del front, los atributos deben tener el MISMO nombre!!
      if (user == null || user.Password == null || user.UserName == null)
        return BadRequest("Verifique los datos a enviar.");

      var userDb = await _loginCrudService.Login(user);

      if (userDb == null)
        return Unauthorized(new ResultJson() { Message = "Usuario y/o contraseña invalido/s" });
      else
        return Ok(new ResultJson() { Message = userDb.DefaultPage });
    }


    //hacer Crear Usuario
  }
}