using ClubHub.Application.DTOs.Members;
using ClubHub.Application.DTOs.SeasonCards;
using ClubHub.Application.Interfaces;
using ClubHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubHub.Application.Services;

public class MemberService(IAppDbContext dbContext) : IMemberService
{
	private IQueryable<GetMemberDto> GetMembersQuery()
	{
		return dbContext.Members
			.AsNoTracking()
			.Select(x => new GetMemberDto
			{
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
				Phone = x.Phone,
				BirthDate = x.BirthDate,
				Id = x.Id,
				CreatedAt = x.CreatedAt,
				SeasonCards = x.SeasonCards.Select(y => new SeasonCardBaseDto
				{
					Id = y.Id,
					NfcKey = y.NfcKey,
					AmountPaid = y.AmountPaid,
					FreeDrinksTotal = y.FreeDrinksTotal,
					FreeDrinksUsed = y.FreeDrinksUsed,
					ValidFrom = y.ValidFrom,
					ValidUntil = y.ValidUntil,
					Status = y.Status
				})
			});
	}

	public async Task<List<GetMemberDto>> GetAllMembers(CancellationToken cancellationToken)
	{
		return await GetMembersQuery().ToListAsync(cancellationToken);
	}

	public async Task<GetMemberDto?> GetMemberById(Guid id, CancellationToken cancellationToken)
	{
		return await GetMembersQuery().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<Guid> CreateMember(MemberBaseDto dto, CancellationToken cancellationToken)
	{
		var member = new Member
		{
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			Email = dto.Email,
			Phone = dto.Phone,
			BirthDate = dto.BirthDate,
		};

		dbContext.Members.Add(member);
		await dbContext.SaveChangesAsync(cancellationToken);
		
		return member.Id;
	}

	public async Task<bool> UpdateMember(Guid id, MemberBaseDto dto, CancellationToken cancellationToken)
	{
		var affected = await dbContext.Members
			.Where(x => x.Id == id)
			.ExecuteUpdateAsync(x => x
				.SetProperty(y => y.FirstName, dto.FirstName)
				.SetProperty(y => y.LastName, dto.LastName)
				.SetProperty(y => y.Email, dto.Email)
				.SetProperty(y => y.Phone, dto.Phone)
				.SetProperty(y => y.BirthDate, dto.BirthDate), cancellationToken);
		
		return affected > 0;
	}

	public async Task<bool> DeleteMember(Guid id, CancellationToken cancellationToken)
	{
		var affected = await dbContext.Members
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync(cancellationToken);
		
		return affected > 0;
	}
}