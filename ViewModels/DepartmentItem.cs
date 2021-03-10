using System.Collections.Generic;

namespace Simpl.ViewModels
{
	public class DepartmentItem
	{
		public string Name { get; set; }
		public IEnumerable<string> Wells { get; set; }
	}
}
