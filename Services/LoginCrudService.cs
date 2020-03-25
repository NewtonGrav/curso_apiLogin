using Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Context;
using Model.Model;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	/*
	 ** Atributos necesarios (Todos readonly --> Para impedir modificaciones)
	 * - El acceso a datos utilizando mi Context del Model
	 * - Realizar informes por consola con el Logger
	 * 
	 ** El contructor utilizara la inyeccion de dependencias para ambos
	 * 
	 ** Los metodos devuelven los Models utilizados
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
			//**** Realizar la consulta con EF ****

			//Find: Busca registro por ID (Se utiliza Linq)
			var queryUser = await _myContext.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();

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
			//Verificar si existe el usuario
			var userExist = await _myContext.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();

			if (userExist != null)
				return null;

			var state = await _myContext.Users.AddAsync(new User() {
				UserName = user.UserName, 
				Password = user.Password, 
				LastLoginDate = DateTime.Now,
				DefaultPage = user.DefaultPage
			});

			await _myContext.SaveChangesAsync();

			return state.Entity;
		}

		public async Task<User> UpdatePassword(UserDTO userToUpdate)
		{
			throw new NotImplementedException();
		}

		public async Task<User> DeleteUser(UserDTO user)
		{
			throw new NotImplementedException();
		}

	}
}
