using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
		//Nombre de mi Politica de CORS
		private readonly string MyAllowSpecificOrigins = "_myPolicy_";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			//**** Mi config ****
			//CORS
			services.AddCors(options => {
				options.AddPolicy(MyAllowSpecificOrigins,
					builder =>
					{
						builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
						//** Otras opciones
						//- builder.WithOrigins("https://localhost:44326");
						//- .WithHeaders("application/json", "application/json; charset=utf-8", "content-type")
						//- .WithMethods("GET","POST","PUT");
					});
			});

			//Service
			services.AddScoped<ILoginCrudService, LoginCrudService>();

			//Obtener connectionString
			string connectionStringNotebook = this.Configuration.GetConnectionString("notebookDb");
			string connectionStringPc = this.Configuration.GetConnectionString("LocalHostDb");

			//Añadir Context y connectionString
			services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionStringPc));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext context)
		{
			//Crea la BD si no esta creada, sino nada
			context.Database.EnsureCreated();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			// Habilitar CORS
			app.UseCors(MyAllowSpecificOrigins);

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//* Mis archivos

		}
	}
}
