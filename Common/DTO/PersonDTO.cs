using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class PersonDTO
	{
		private long _dni;
		private string _name;
		private string _surName;

		public long Dni { get => _dni; set => _dni = value; }
		public string Name { get => _name; set => _name = value; }
		public string SurName { get => _surName; set => _surName = value; }
	}
}
