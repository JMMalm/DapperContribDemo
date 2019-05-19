using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperContribDemo.Core.Extensions
{
	public class StringExtensions
	{
		public static bool AnyNullOrWhiteSpace(params string[] values)
		{
			if (values == null || values.Length == 0)
				return true;

			return values.Any(v => string.IsNullOrWhiteSpace(v));
		}
	}
}
