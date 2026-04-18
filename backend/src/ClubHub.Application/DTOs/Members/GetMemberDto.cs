using ClubHub.Application.DTOs.SeasonCards;

namespace ClubHub.Application.DTOs.Members;

public class GetMemberDto : MemberBaseDto
{
	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public IEnumerable<SeasonCardBaseDto> SeasonCards { get; set; } = [];
}