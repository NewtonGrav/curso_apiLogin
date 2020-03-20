using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Context;
using Services;
using Services.Interfaces;

namespace Practica_Clase21_Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			//**** Mi confing ****
			//CORS


			//Inyeccion de dependencia de los Service
			services.AddScoped<ILoginCrudService, LoginCrudService>();
			services.AddScoped<ITableService, TableService>();

			//??
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			//Obtener connectionString
			string connectionString = this.Configuration.GetConnectionString("LocalHostDb");
			//Añadir Context y connectionString
			services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//* Mis archivos

		}
	}
}
