using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Practica_Clase21_Api.Controllers
{
  [Route("api/Table")]
  [ApiController]
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
      //Se toman la lista de personas
      var tablePersons = await _tableService.GetPersons();

      if (tablePersons == null)
        return Unauthorized("No pudo obtenerse los datos");

      return Ok(tablePersons);
    }
  }
}