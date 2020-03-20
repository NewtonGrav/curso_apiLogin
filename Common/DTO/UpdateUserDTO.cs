using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class UpdateUserDTO
	{
		private long _id;
		private string _userName;
		private string _newUserName;

		private string _password;
		private string _newPassword;

		public long Id { get => _id; set => _id = value; }
		public string UserName { get => _userName; set => _userName = value; }
		public string NewUserName { get => _newUserName; set => _newUserName = value; }
		public string Password { get => _password; set => _password = value; }
		public string NewPassword { get => _newPassword; set => _newPassword = value; }
	}
}
