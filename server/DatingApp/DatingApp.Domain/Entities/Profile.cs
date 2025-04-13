using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class Profile
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Age { get; set; } = 0;
		public string Town { get; set; } = string.Empty;
		public bool Gender { get; set; } = false;
		public BigInteger Balance { get; set; } = 0;
		public User User { get; set; }
		public ICollection<Hobby>? Hobbies { get; set; } = new List<Hobby>();
		public ICollection<Achievement>? Achievements { get; set; } = new List<Achievement>();

		public Profile() { }
		public Profile(Guid id, string name, string description, int age, string town, BigInteger balance)
		{
			Id = id;
			Name = name;
			Description = description;
			Age = age;
			Town = town;
			Balance = balance;
		}
	}
}
