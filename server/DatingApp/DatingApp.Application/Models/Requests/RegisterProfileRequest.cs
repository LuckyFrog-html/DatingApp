using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.Requests
{
	public record class RegisterProfileRequest
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[Range(12, 150)]
		public int Age { get; set; }

		[Required]
		[MaxLength(50)]
		public string Town { get; set; }

		[Required]
		public bool Gender { get; set; }
	}
}
