namespace ClubHub.Application.DTOs.Members;

public class MemberBaseDto
{
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required string Email { get; set; }
	public string? Phone { get; set; }
	public DateOnly? BirthDate { get; set; }
}