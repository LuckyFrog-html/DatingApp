using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
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
		[JsonIgnore]
		public User User { get; set; }
		public bool IsDeleted { get; set; } = false;
		
		[JsonIgnore]
		public ICollection<Hobby>? Hobbies { get; set; } = new List<Hobby>();
		[JsonIgnore]
		public ICollection<Achievement>? Achievements { get; set; } = new List<Achievement>();

		public Profile() { }
		public Profile(Guid id, string name, string description, int age, string town, BigInteger balance, bool gender)
		{
			Id = id;
			Name = name;
			Description = description;
			Age = age;
			Town = town;
			Gender = gender;
			Balance = balance;
		}
	}
}
