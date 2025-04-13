using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Core.Models
{
	public class CodeConfirmation
	{
		public Guid Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime ExpiresAt { get; set; }

		public CodeConfirmation() { }

		public CodeConfirmation(string email, string code, TimeSpan expiresTime)
		{
			Email = email;
			Code = code;
			CreatedAt = DateTime.UtcNow;
			ExpiresAt = CreatedAt + expiresTime;
		}
	}
}
