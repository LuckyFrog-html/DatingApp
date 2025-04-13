using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class Achievement
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Achievement() { }
		public Achievement(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
