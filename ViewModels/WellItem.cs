using System.Collections.Generic;

namespace Simpl.ViewModels
{
	public class WellItem
	{
		public string Well { get; set; }
		public IEnumerable<WellItemParameter> Parameters { get; set; }
	}
}
