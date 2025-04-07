using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IProfileRepository _profileRepository;
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRoleRepository _roleRepository;

		public UserService(
		   IProfileRepository profileRepository,
		   IUserRepository userRepository,
		   IUnitOfWork unitOfWork,
		   IRoleRepository roleRepository
		   )
		{
			_profileRepository = profileRepository;
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
			_roleRepository = roleRepository;
		}



		public async Task<ErrorOr<Success>> CreateUserAsync(RegisterRequest registerRequest, 
			CancellationToken cancellationToken)
		{
			User user = new User
			{
				Id = Guid.NewGuid(),
				Name = registerRequest.Username,
				Email = registerRequest.Email,
				Password = registerRequest.Password,
				CreatedAt = DateTime.UtcNow,

			};
			Profile profile = new Profile
			{
				Id = user.Id,
				Name = registerRequest.Name,
				Age = registerRequest.Age,
				Town = registerRequest.Town,
				Gender = registerRequest.Gender,
			};

			user.Profile = profile;

			var roleResult = await _roleRepository.GetByNameAsync("user", cancellationToken);
			if (roleResult.IsError)
			{
				return roleResult.Errors;
			}

			user.Roles.Add(roleResult.Value);

			try
			{
				await _unitOfWork.BeginTransactionAsync(cancellationToken);
				await _userRepository.AddAsync(user, cancellationToken);
				await _profileRepository.AddAsync(profile, cancellationToken);
				await _unitOfWork.SaveChangesAsync(cancellationToken);
				await _unitOfWork.CommitAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync(cancellationToken);
				return Error.Failure("CreateUserFailure", description: ex.Message);
			}
		}

	}
}
