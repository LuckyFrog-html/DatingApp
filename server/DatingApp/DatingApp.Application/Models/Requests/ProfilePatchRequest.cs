using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.Requests
{
	public record ProfilePatchRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Age { get; set; }
		public string Town { get; set; }
	}
}
