using Dapper.Contrib.Extensions;
using System;
using System.Data.SqlClient;

namespace DapperContribDemo.Core
{
	[Table("Users")]
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime LastModified { get; set; }

		public User()
		{

		}
	}
}
