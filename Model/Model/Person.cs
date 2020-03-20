using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Model
{
	public class Person
	{
		[MaxLength(8), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Dni { get; set; }

		[MaxLength(20)]
		public string FullName { get; set; }
	}
}
