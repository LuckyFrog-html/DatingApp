﻿using DatingApp.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IHobbyRepository : IRepository<Hobby>
	{
		Task<ErrorOr<User>> GetByNameAsync(string name, CancellationToken cancellationToken);
	}
}
