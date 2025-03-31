using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class Hobby
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; } = string.Empty;
		public Hobby() { }
		public Hobby(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
