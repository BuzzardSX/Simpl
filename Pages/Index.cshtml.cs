using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;
using static System.Math;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simpl.Extensions;
using Simpl.Models;
using Simpl.ViewModels;

namespace Simpl.Pages
{
	public class IndexModel : PageModel
	{
		public IndexModel(IRepository<WellParameter> wellParameters, IRepository<Well> wells, IRepository<Department> departments)
		{
			WellParameters = wellParameters;
			Wells = wells;
			Departments = departments;
		}
		public IEnumerable<Well> Wells { get; }
		public IEnumerable<WellParameter> WellParameters { get; }
		public IEnumerable<Department> Departments { get; }
		public void OnGet() { }
		public IActionResult OnGetWellParameterList()
		{
			var model = WellParameters.Select(p => p.ParameterName).Distinct();
			return Partial("_WellParameterList", model);
		}
		public IActionResult OnGetWellList()
		{
			var model = Wells
				.Where(w => w.Id >= 10 && w.Id <= 30)
				.GroupJoin(WellParameters, w => w.Id, p => p.WellId, (w, ps) => new WellItem
				{
					Well = w.Name,
					Parameters = ps
						.GroupBy(p => p.ParameterName, w => w.Value, (n, vs) => new WellItemParameter
						{
							Name = n,
							Minimun = vs.Min(),
							Maximun = vs.Max(),
							Average = vs.Average(),
							Median = vs.Median()
						})
				});
			return Partial("_WellList", model);
		}
		public IActionResult OnGetDepartmentList()
		{
			var departments =
				from d in Departments
				from w in Wells
				where w.X.HasValue && w.Y.HasValue && d.Radius >= Sqrt(Pow(d.X - w.X.Value, 2) + Pow(d.Y - w.Y.Value, 2))
				group (d, w) by d.Name into g
				select new DepartmentItem
				{
					Name = g.Key,
					Wells = g.Select(g => g.w.Name)
				};
			var wells = departments
				.Select(d => d.Wells)
				.Aggregate((wsA, wsB) => Enumerable.Union(wsA, wsB));
			var unknownDepartment = new DepartmentItem
			{
				Name = "Неизвестное месторождение",
				Wells = Wells.Select(w => w.Name).Except(wells)
			};
			var emptyDepartments = Departments
				.Select(d => d.Name)
				.Except(departments.Select(d => d.Name))
				.Select(n => new DepartmentItem { Name = n });
			var model = Empty<DepartmentItem>()
				.Concat(departments)
				.Append(unknownDepartment)
				.Concat(emptyDepartments);
			return Partial("_DepartmentList", model);
		}
	}
}
