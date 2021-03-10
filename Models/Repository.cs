using static System.Char;
using System.Collections;
using System.Collections.Generic;
using static System.IO.File;
using static System.IO.Path;
using static System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.Hosting;

namespace Simpl.Models
{
	public class Repository<T> : IRepository<T>
	{
		private readonly IEnumerable<T> source;
		public Repository(IWebHostEnvironment env)
		{
			var type = typeof(T).Name;
			var fileName = ToUpper(type[0]) + type.Substring(1);
			var json = ReadAllText(path: Combine(env.ContentRootPath, "App_Data", $"{fileName}s.json"));
			source = Deserialize<IEnumerable<T>>(json);
		}
		public IEnumerator<T> GetEnumerator() => source.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
