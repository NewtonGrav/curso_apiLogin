using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Common.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Practica_Clase21_Api.Controllers
{
  [Route("api/Table")]
  [ApiController]
  [EnableCors("_myPolicy_")]
  public class TableController : ControllerBase
  {
    private ILogger<TableController> _logger;
    private ITableService _tableService;

    public TableController(ILogger<TableController> logger, ITableService tableService)
    {
      _logger = logger;
      _tableService = tableService;

      _logger.LogInformation("Constructor TableController");
    }

    [HttpGet("GetPersons")]
    public async Task<ActionResult> GetPersons()
    {
      var tablePersons = await _tableService.GetPersons();

      if (tablePersons == null)
        return Unauthorized("No pudo obtenerse los datos");

      return Ok(tablePersons);
    }

    [HttpPut("AddPerson")]
    public async Task<ActionResult> AddPerson([FromBody] PersonDTO person)
    {
      if (person.FullName == "" || person.Dni == "")
        return BadRequest("Verifique los datos a enviar");

      var personAdded = await _tableService.AddPerson(person);

      if (personAdded == null)
        return Unauthorized("El usuario ya existe");

      return Ok(personAdded);
    }

    [HttpDelete("DeletePerson")]
    public async Task<ActionResult> DeletePerson(string dniPerson)
    {
      if (dniPerson == null || dniPerson == "")
        return BadRequest(new ResultJson() { 
          Message = "No se detecto la persona a eliminar" 
        });

      int rowsAffected = await _tableService.DeletePerson(dniPerson);

      if (rowsAffected == 0)
        return Unauthorized(new ResultJson() {
          Message = "No existe o no se pudo eliminar la persona" 
        });

      return Ok(new ResultJson() {
        Message = $"La persona con DNI {dniPerson} se a eliminado."
      });
    }
  }
}