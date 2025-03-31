using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.Requests
{
	public class LoginRequest
	{
		[Required]
		[MaxLength(50)]
		public string Username { get; set; }

		[Required]
		[MaxLength(50)]
		public string Password { get; set; }
	}
}
