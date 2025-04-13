using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.Requests
{
	public class PartnerProfileRequest
	{
		[Required]
		[MaxLength(50)]
		public Guid Id { get; set; }
	}
}
