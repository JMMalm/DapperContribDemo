using Dapper.Contrib.Extensions;
using DapperContribDemo.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DapperContribDemo.Infrastructure.Repositories
{
	public class UserRepository
	{
		private readonly IConfiguration _config;

		public UserRepository(IConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("Configuration cannot be null.");
			}
			_config = config;
		}

		public  User GetUser(int id)
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				return connection.Get<User>(id);
			}
		}

		public  IEnumerable<User> GetUsers()
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				return connection.GetAll<User>();
			}
		}

		public  long InsertUser(User user)
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				return connection.Insert(user);
			}
		}

		public  long InsertUsers(IEnumerable<User> users)
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				return connection.Insert(users);
			}
		}

		public bool UpdateUser(User user)
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				user.LastModified = DateTime.Now;
				return connection.Update<User>(user);
			}
		}

		public  bool DeleteUser(User user)
		{
			using (var connection = new SqlConnection(_config.GetConnectionString("DapperContribDemo")))
			{
				connection.Open();
				return connection.Delete<User>(user);
			}
		}
	}
}
