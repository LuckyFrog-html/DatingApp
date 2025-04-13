using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class Role
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<User> Users { get; set; } = new List<User>();
		public Role() { }
		public Role(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
