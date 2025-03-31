using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.DTOs
{
	public class UserDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Age { get; set; }
		public string Town { get; set; }
		public IEnumerable<Hobby> Hobbies { get; set; }

	}
}
