using Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Context;
using Model.Model;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
	public class TableService : ITableService
	{
		private readonly ILogger<TableService> _logger;
		private readonly MyContext _myContext;

		public TableService(ILogger<TableService> logger, MyContext myContext)
		{
			_logger = logger;
			_myContext = myContext;
			_logger.LogInformation("Contructor TableService");
		}

		public async Task<List<Person>> GetPersons()
		{
			var tablePersons = await _myContext.Persons.ToListAsync();

			return tablePersons;
		}

		public async Task<Person> AddPerson(PersonDTO person)
		{
			Person newPerson = new Person() { FullName = person.FullName, Dni = person.Dni };

			try
			{
				var state = await _myContext.Persons.AddAsync(newPerson);
				_myContext.SaveChanges();
			} catch(Exception e)
			{
				_logger.LogWarning($"Error: {e.InnerException.Message}\nDate: {DateTime.Now} ");
				return null;
			}

			return newPerson;
		}

		public async Task<int> DeletePerson(string dniPerson)
		{
			var person = await _myContext.Persons
				.Where((p) => p.Dni == dniPerson)
				.FirstOrDefaultAsync();

			if (person == null)
				return 0;

			_myContext.Persons.Remove(person);
			int rowsAffected = await _myContext.SaveChangesAsync();

			return rowsAffected;
		}
	}
}
