using Dapper.Contrib.Extensions;
using DapperContribDemo.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace DapperContribDemo.Core
{
	[Table("Users")]
	public class User
	{
		[Dapper.Contrib.Extensions.Key]
		public int Id { get; set; }

		[Display(Name = "First Name")]
		[DataType(DataType.Text)]
		[Required]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		[DataType(DataType.Text)]
		[Required]
		public string LastName { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		[Required]
		public string Email { get; set; }

		[Display(Name = "Last Modified")]
		[DataType(DataType.Date)]
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
