using DatingApp.Application.Models.DTOs;
using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Models.Responses
{
	public record LoginResponse
	(
		string AccessToken,
		string RefreshToken
	);
}
