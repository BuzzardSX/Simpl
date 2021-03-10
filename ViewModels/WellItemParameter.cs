using System.ComponentModel.DataAnnotations;

namespace Simpl.ViewModels
{
	public class WellItemParameter
	{
		public string Name { get; set; }
		[DisplayFormat(DataFormatString = "{0:F2}")]
		public double Minimun { get; set; }
		[DisplayFormat(DataFormatString = "{0:F2}")]
		public double Maximun { get; set; }
		[DisplayFormat(DataFormatString = "{0:F2}")]
		public double Average { get; set; }
		[DisplayFormat(DataFormatString = "{0:F2}")]
		public double Median { get; set; }
	}
}
