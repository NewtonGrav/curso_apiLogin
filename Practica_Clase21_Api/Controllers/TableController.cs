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
  }
}