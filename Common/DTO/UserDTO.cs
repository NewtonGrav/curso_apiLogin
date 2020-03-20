using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class UserDTO
	{
		private long _id;
		private string _userName;
		private string _password;
		private DateTime _lastLoginDate;
		private string _defaultPage;

		public long Id { get => _id; set => _id = value; }
		public string UserName { get => _userName; set => _userName = value; }
		public string Password { get => _password; set => _password = value; }
		public DateTime LastLoginDate { get => _lastLoginDate; set => _lastLoginDate = value; }
		public string DefaultPage { get => _defaultPage; set => _defaultPage = value; }
	}
}
