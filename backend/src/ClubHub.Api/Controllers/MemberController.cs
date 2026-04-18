using ClubHub.Application.DTOs.Members;
using ClubHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClubHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController(IMemberService memberService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<List<GetMemberDto>>> GetAllMembers(CancellationToken cancellationToken)
	{
		return Ok(await memberService.GetAllMembers(cancellationToken));
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<GetMemberDto>> GetMemberById([FromRoute] Guid id,
		CancellationToken cancellationToken)
	{
		var member = await memberService.GetMemberById(id, cancellationToken);
		if (member == null)
			return NotFound();

		return Ok(member);
	}

	[HttpPost]
	public async Task<ActionResult> CreateMember([FromBody] MemberBaseDto dto, CancellationToken cancellationToken)
	{
		var id = await memberService.CreateMember(dto, cancellationToken);
		return CreatedAtAction(nameof(GetMemberById), new { id }, null);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult> UpdateMember([FromRoute] Guid id, [FromBody] MemberBaseDto dto,
		CancellationToken cancellationToken)
	{
		var updated = await memberService.UpdateMember(id, dto, cancellationToken);
		if (!updated) return NotFound();
		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> DeleteMember([FromRoute] Guid id, CancellationToken cancellationToken)

	{
		var isDeleted = await memberService.DeleteMember(id, cancellationToken);
		if (!isDeleted) return NotFound();
		return NoContent();
	}
}