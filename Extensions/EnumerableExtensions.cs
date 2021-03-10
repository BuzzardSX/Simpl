using System.Collections.Generic;
using System.Linq;

namespace Simpl.Extensions
{
	public static class EnumerableExtensions
	{
		public static double Median(this IEnumerable<double> source)
		{
			var med = source.Count() / 2;
			var ordered = source.OrderBy(i => i);
			if (source.Count() % 2 == 1)
			{
				return ordered.ElementAt(med);
			}
			else
			{
				return (ordered.ElementAt(med - 1) + ordered.ElementAt(med)) / 2;
			}
		}
	}
}
