using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class UserUpdateDTO
	{
		private UserDTO user;
		private string newPassword;

		public UserDTO User { get => user; set => user = value; }
		public string NewPassword { get => newPassword; set => newPassword = value; }
	}
}
