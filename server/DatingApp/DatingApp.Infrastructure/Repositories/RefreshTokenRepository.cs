using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Repositories
{
	internal class RefreshTokenRepository : IRefreshTokenRepository
	{
		private readonly ApplicationContext _dbContext;

		public RefreshTokenRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ErrorOr<Success>> AddAsync(RefreshToken entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				await _dbContext.SaveChangesAsync();
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add refresh token. Error: {ex.Message}");
			}
		
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new RefreshToken { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete refresh token. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<RefreshToken>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var tokens = await _dbContext.Set<RefreshToken>()
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return tokens;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get refresh tokens. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<RefreshToken>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var token = await _dbContext.Set<RefreshToken>()
					.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

				return token is not null
					? token
					: Error.NotFound(description: "Refresh token not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get refresh tokens. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(RefreshToken entity, CancellationToken cancellationToken)
		{
			try
			{
				var existingToken = await _dbContext.Set<RefreshToken>()
					.AnyAsync(t => t.Id == entity.Id, cancellationToken);

				if (!existingToken)
				{
					return Error.NotFound(description: "Refresh token not found");
				}

				_dbContext.Update(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update refresh token. Error: {ex.Message}");
			}
		}
	}
}
