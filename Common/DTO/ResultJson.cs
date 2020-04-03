using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class ResultJson
	{
		private string _message;
		private string guid;

		public string Message { get => _message; set => _message = value; }
		public string Guid { get => guid; set => guid = value; }
	}
}
