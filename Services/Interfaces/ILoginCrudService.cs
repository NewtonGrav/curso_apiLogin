using Common.DTO;
using Model.Model;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface ILoginCrudService
	{
		Task<User> Login(UserDTO queryUser);
		Task<User> UpdatePassword(UserUpdateDTO userToUpdated);
		Task<User> CreateUSer(UserDTO user);
	}
}
