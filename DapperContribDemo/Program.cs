using Dapper.Contrib.Extensions;
using DapperContribDemo.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using DapperContribDemo.Infrastructure.Repositories;

namespace DapperContribDemo
{
	public class Program
	{
		private static IConfigurationRoot _config;

		static void Main(string[] args)
		{
			try
			{

				_config = InitializeConfiguration();
				UserRepository userRepo = new UserRepository(_config);

				//var user = GetUser(1);
				//Console.WriteLine($"User {user.FirstName} {user.LastName}");
				//Console.WriteLine($"Email {user.Email}");

				var newUserId = userRepo.InsertUser(new User("Jane", "Doe", "jdoe@gmail.com"));
				Console.WriteLine($"Inserted user. ID: {newUserId}");

				var users = userRepo.GetUsers();
				foreach(User user in users)
				{
					Console.WriteLine($"ID: {user.Id}; Name: {user.FirstName} {user.LastName}; Email: {user.Email}");
				}

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

		private static IConfigurationRoot InitializeConfiguration()
		{
			/*
				Remember to install the following Nuget packages for Configuration:
					Microsoft.Extensions.Configuration
					Microsoft.Extensions.Configuration.FileExtensions
					Microsoft.Extensions.Configuration.Json

				Remember to set the appSettings.json to always copy output during build, via its properties.

				Dapper Contrib docs:
					https://dapper-tutorial.net/dapper-contrib

				Using the .NET Framework style ConfigurationManager:
					https://stackoverflow.com/questions/47591910/is-configurationmanager-appsettings-available-in-net-core-2-0
			*/
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
				.Build();
		}
	}
}
