using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Model
{
	public class Person
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(TypeName = "bigint")]
		public long DNI { get; set; }

		[MaxLength(50)]
		[Required]
		public string Name { get; set; }

		[MaxLength(50)]
		[Required]
		public string SurName { get; set; }
	}
}
