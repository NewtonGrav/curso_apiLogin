using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class PersonDTO
	{
		private string _dni;
		private string _fullName;

		public string Dni { get => _dni; set => _dni = value; }
		public string FullName { get => _fullName; set => _fullName = value; }
	}
}
