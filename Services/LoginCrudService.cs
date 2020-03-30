﻿using Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Context;
using Model.Model;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
	/*
	 ** Atributos necesarios (readonly --> Para impedir modificaciones)
	 * - El acceso a datos utilizando mi Context del Model
	 * - Realizar informes por consola con el Logger
	 * 
	 ** El contructor utilizara la inyeccion de dependencias para ambos
	 * 
	 * **/

	public class LoginCrudService : ILoginCrudService
	{

		private readonly ILogger<LoginCrudService> _logger;
		private readonly MyContext _myContext;

		public LoginCrudService(ILogger<LoginCrudService> logger, MyContext myContext)
		{
			_logger = logger;
			_myContext = myContext;

			_logger.LogInformation("Contructor LoginCrudService");
		}

		public async Task<User> Login(UserDTO user)
		{
			var queryUser = await _myContext.Users
				.Where(u => u.UserName == user.UserName)
				.FirstOrDefaultAsync();

			if (queryUser == null)
				return null;

			if (queryUser.UserName != null && queryUser.Password == user.Password)
			{
				queryUser.LastLoginDate = DateTime.Now;
				await _myContext.SaveChangesAsync();

				_logger.LogInformation("Se realizo login. Se actualizo la fecha de ultimo logeo");

				return queryUser;
			}

			return null;
		}

		public async Task<User> CreateUSer(UserDTO user)
		{
			var userExist = await _myContext.Users
				.Where(u => u.UserName == user.UserName)
				.FirstOrDefaultAsync();

			if (userExist != null)
			{
				_logger.LogWarning($"Error: El usuario ya existe" +
													$"\nDate: {DateTime.Now} ");
				return null;
			}

			var state = await _myContext.Users.AddAsync(new User() {
				UserName = user.UserName, 
				Password = user.Password, 
				LastLoginDate = DateTime.Now,
				DefaultPage = user.DefaultPage
			});

			await _myContext.SaveChangesAsync();

			return state.Entity;
		}

		public async Task<User> UpdatePassword(UserUpdateDTO userToUpdated)
		{
			var userExist = await _myContext.Users.Where(
				u => u.UserName == userToUpdated.User.UserName && u.Password == userToUpdated.User.Password
				).FirstOrDefaultAsync();

			if (userExist == null)
			{
				_logger.LogWarning($"Error: No se cambio ninguna contraseña. El usuario no existe" +
													$"\nDate: {DateTime.Now} ");
				return null;
			}

			userExist.Password = userToUpdated.NewPassword;
			await _myContext.SaveChangesAsync();

			return userExist;
		}


	}
}
