using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Model
{
	public class User
	{
		//PK
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(TypeName = "bigint")]
		public long Id { get; set; }

		//No Nulo
		[MaxLength(20)]
		[Required]
		public string UserName { get; set; }

		//No Nulo
		[MaxLength(50)]
		[Required]
		public string Password { get; set; }

		[DefaultValue(null)]
		public DateTime? LastLoginDate { get; set; }

		//No Nulo
		[MaxLength(50)]
		[Required]
		public string DefaultPage { get; set; }

		[Required]
		public string Guid { get; set; }
	}
}
