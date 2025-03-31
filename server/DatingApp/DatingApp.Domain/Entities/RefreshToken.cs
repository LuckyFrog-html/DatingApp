using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class RefreshToken
	{
		public Guid Id { get; set; }
		public string Value { get; set; }
		public DateTime CreatedAt { get; set; }
		public User User { get; set; }

		public RefreshToken() { }

		public RefreshToken(Guid id, string value, DateTime createdAt)
		{
			Id = id;
			Value = value;
			CreatedAt = createdAt;
		}
	}
}
