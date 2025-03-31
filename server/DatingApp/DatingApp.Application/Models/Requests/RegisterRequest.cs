using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.Requests
{
	public class RegisterRequest
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		public int Age { get; set; }

		[Required]
		[MaxLength(50)]
		public string Town { get; set; }

		[Required]
		[MaxLength(50)]
		public string Username {  get; set; }
		
		[Required]
		[MaxLength(50)]
		public string Email { get; set; }

		[Required]
		[MaxLength(50)]
		public string Password { get; set; }
	}
}
