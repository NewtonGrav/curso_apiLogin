﻿using System;
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
			bool algunCampoRequeridoInvalido = user == null || user.Password == null || user.UserName == null;
			if (algunCampoRequeridoInvalido)
				return BadRequest("Verifique los datos a enviar.");

			var userDb = await _loginCrudService.Login(user);

			if (userDb == null)
				return Unauthorized(new ResultJson() { Message = "Usuario y/o contraseña invalido/s" });
			else
				return Ok(new ResultJson() { Message = userDb.DefaultPage });
		}

		[HttpPost("CreateUser")]
		public async Task<ActionResult> CreateUser([FromBody] UserDTO user)
		{
			bool algunCampoRequeridoInvalido = user == null || user.Password == null || 
				user.UserName == null || user.DefaultPage == null;

			if (algunCampoRequeridoInvalido)
				return BadRequest("Verifique los datos a enviar.");

			var userDb = await _loginCrudService.CreateUSer(user);

			if (userDb == null)
				return Unauthorized(new ResultJson() { Message = $"El usuario \"{user.UserName}\" ya existe." });

			return Ok(new ResultJson() { Message = "Usuario creado con exito :)" });
		}

		[HttpPut("UpdatePassword")]
		public async Task<ActionResult> UpdatePassword([FromBody] UserUpdateDTO userToUpdated)
		{
		
			bool algunCampoRequeridoInvalido = (userToUpdated.User == null) || 
				(userToUpdated.User.Password == null) || (userToUpdated.User.UserName == null);
			if (algunCampoRequeridoInvalido || userToUpdated.NewPassword == "")
				return BadRequest(new ResultJson() { Message = "Verifique los datos a enviar" });

			bool esMismaContrasenia = userToUpdated.User.Password.Equals(userToUpdated.NewPassword);
			if (esMismaContrasenia)
				return BadRequest(new ResultJson() { Message = "La nueva contraseña no puede ser igual a la actual" });

			var userDb = await _loginCrudService.UpdatePassword(userToUpdated);

			if (userDb == null)
				return Unauthorized(new ResultJson() { 
					Message = $"No pudo cambiarse la contraseña.\n" +
										$"El usuario no existe o la contraeña no corresponde al mismo" 
				});

			return Ok(new ResultJson() { Message = "Cambio de contraeña realizado :)" });
		}

	}
}