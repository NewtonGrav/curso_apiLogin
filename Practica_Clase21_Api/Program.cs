using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Practica_Clase21_Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		//Servidor Web
		public static IWebHostBuilder CreateHostBuilder(String[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.ClearProviders();
					logging.SetMinimumLevel(LogLevel.Trace);
					logging.AddConsole();
				})
				.UseStartup<Startup>();

		}

		//Servidor generico
		//public static IHostBuilder CreateHostBuilder(string[] args) =>
		//		Host.CreateDefaultBuilder(args)
		//				.ConfigureWebHostDefaults(webBuilder =>
		//				{
		//					webBuilder.UseStartup<Startup>();

		//				});
	}
}
