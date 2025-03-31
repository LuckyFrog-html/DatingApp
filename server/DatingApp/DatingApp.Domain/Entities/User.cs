using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastLogin{ get; set; }
		public ICollection<Role> Roles { get; set; } = new List<Role>();
		public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
		public Profile Profile { get; set; }

		public User() { }

		public User(Guid id, string name, string email, string password, DateTime createdAt, DateTime lastLogin)
		{
			Id = id;
			Name = name;
			Email = email;
			Password = password;
			CreatedAt = createdAt;
			LastLogin = lastLogin;
		}
	}
}
