using Dapper.Contrib.Extensions;
using System;
using System.Data.SqlClient;
using DapperContribDemo.Core.Extensions;

namespace DapperContribDemo.Core
{
	[Table("Users")]
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime LastModified { get; set; }

		[Write(false)]
		[Computed]
		public string FakeProperty { get; set; }

		public User() { }
		public User(string firstName, string lastName, string email, DateTime? lastModified = null)
		{
			if (StringExtensions.AnyNullOrWhiteSpace(firstName, lastName, email))
			{
				throw new ArgumentNullException("All arguments must not be null or whitespace.");
			}

			FirstName = firstName;
			LastName = lastName;
			Email = email;
			LastModified = lastModified ?? DateTime.Now;
		}
	}
}
