using Common.DTO;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface ILoginCrudService
	{
		Task<User> Login(UserDTO queryUser);
		Task<User> UpdatePassword(UpdateUserDTO userToUpdate);
		Task<User> CreateUSer(UserDTO user);
		Task<User> DeleteUser(UserDTO user);
	}
}
