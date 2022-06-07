using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace EcoCar
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					if (System.Diagnostics.Debugger.IsAttached)
					{
						Console.WriteLine("Debug mode");
						webBuilder.UseStartup<Startup>();
					}
					else
					{
						Console.WriteLine("Release mode");
						webBuilder.UseStartup<Startup>().UseUrls("http://0.0.0.0:5000", "http://0.0.0.0:5001");
					}
				});
	}
}
