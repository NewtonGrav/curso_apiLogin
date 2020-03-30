using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Model.Model;
using System.Linq;

namespace Model.Context
{
	/*
	 * - ConnectionString 
	 * - Tablas a mapear
	 * **/
	public class MyContext : DbContext
	{
		private readonly string _connectionString;
		public MyContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public MyContext(DbContextOptions options) : base(options)
		{
			//Busca el connectionString en la configuracion
			_connectionString = ((RelationalOptionsExtension) options
				.Extensions
				.Where(e => e is SqlServerOptionsExtension)
				.FirstOrDefault()).ConnectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString);
				base.OnConfiguring(optionsBuilder);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		//Entidades(tablas) a mapear
		public DbSet<User> Users { get; set; }
		public DbSet<Person> Persons { get; set; }
	}
}
