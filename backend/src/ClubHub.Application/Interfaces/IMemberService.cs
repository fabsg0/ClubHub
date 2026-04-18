using ClubHub.Application.DTOs.Members;

namespace ClubHub.Application.Interfaces;

public interface IMemberService
{
	Task<List<GetMemberDto>> GetAllMembers(CancellationToken cancellationToken);
	Task<GetMemberDto?> GetMemberById(Guid id, CancellationToken cancellationToken);
	Task<Guid> CreateMember(MemberBaseDto dto, CancellationToken cancellationToken);
	Task<bool> UpdateMember(Guid id, MemberBaseDto dto, CancellationToken cancellationToken);
	Task<bool> DeleteMember(Guid id, CancellationToken cancellationToken);
}