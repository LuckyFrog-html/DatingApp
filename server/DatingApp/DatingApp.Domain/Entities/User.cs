using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastLogin{ get; set; }
		public ICollection<Role> Roles { get; set; } = new List<Role>();
		public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
		[JsonIgnore]
		public Profile Profile { get; set; }

		public User() { }

		public User(Guid id, string email, string password, DateTime createdAt, DateTime lastLogin)
		{
			Id = id;
			Email = email;
			Password = password;
			CreatedAt = createdAt;
			LastLogin = lastLogin;
		}
	}
}
