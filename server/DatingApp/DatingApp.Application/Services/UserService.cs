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



		public async Task<ErrorOr<Success>> CreateUserAsync(string email, string password, 
			CancellationToken cancellationToken)
		{
			User user = new User
			{
				Id = Guid.NewGuid(),
				Email = email,
				Password = password,
				CreatedAt = DateTime.UtcNow,
			};

			var roleResult = await _roleRepository.GetByNameAsync("user", cancellationToken);
			if (roleResult.IsError)
			{
				return Error.Failure("GetRoleFailure");
			}

			user.Roles.Add(roleResult.Value);

			try
			{
				await _unitOfWork.BeginTransactionAsync(cancellationToken);
				await _userRepository.AddAsync(user, cancellationToken);
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

		public async Task<ErrorOr<List<User>>> GetAllUsersAsync(CancellationToken cancellationToken)
		{
			var errorOrUsers = await _userRepository.GetAllAsync(cancellationToken);
			if (errorOrUsers.IsError)
			{
				return errorOrUsers.Errors;
			}
			return errorOrUsers.Value;
		}

		public async Task<ErrorOr<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
		{
			var errorOrUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
			if (errorOrUser.IsError)
			{
				return errorOrUser.Errors;
			}
			return errorOrUser.Value;
		}

		public async Task<ErrorOr<bool>> IsUserExists(string email, CancellationToken cancellationToken)
		{
			var errorOrUser = await _userRepository.GetByEmailAsync("email", cancellationToken);
			if (errorOrUser.IsError)
			{
				if (errorOrUser.Errors.First().Type is ErrorType.NotFound) {
					return false;
				}
				return errorOrUser.Errors;
			}
			if (errorOrUser.Value is User)
			{
				return true;
			}
			return false;
		}

	}
}
