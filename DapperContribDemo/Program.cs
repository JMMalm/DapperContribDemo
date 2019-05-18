using Dapper.Contrib.Extensions;
using DapperContribDemo.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace DapperContribDemo
{
	public class Program
	{
		private static IConfigurationRoot _config;

		static void Main(string[] args)
		{
			try
			{
				/*
					Remember to install the following Nuget packages for Configuration:
						Microsoft.Extensions.Configuration
						Microsoft.Extensions.Configuration.FileExtensions
						Microsoft.Extensions.Configuration.Json

					Remember to set the appSettings.json to always copy output during build, via its properties.

					Dapper Contrib docs:
						https://dapper-tutorial.net/dapper-contrib
				*/

				var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

				_config = builder.Build();

				var user = GetUser();

				Console.WriteLine($"User {user.FirstName} {user.LastName}");
				Console.WriteLine($"Email {user.Email}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message}{Environment.NewLine}{ex.InnerException}");
				throw;
			}

			Console.WriteLine($"{Environment.NewLine}Press any key to continue.");
			Console.Beep();
			Console.ReadKey();
		}

		public static User GetUser()
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				return connection.Get<User>(1);
			}
		}
	}
}
