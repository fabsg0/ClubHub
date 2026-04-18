using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClubHub.Domain.Enums;

namespace ClubHub.Domain.Entities;

[Table("season_cards")]
public class SeasonCard
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	[Required] public Guid MemberId { get; set; }

	[Required] [MaxLength(100)] public string NfcKey { get; set; } = string.Empty;

	/// <summary>
	///     Paid in cents
	/// </summary>
	[Required]
	public int AmountPaid { get; set; }

	[Required] public int FreeDrinksTotal { get; set; }

	[Required] public int FreeDrinksUsed { get; set; }

	[Required] public DateOnly ValidFrom { get; set; }

	[Required] public DateOnly ValidUntil { get; set; }

	[Required] [MaxLength(20)] public CardStatus Status { get; set; } = CardStatus.Active;

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public DateTime CreatedAt { get; set; }

	[ForeignKey(nameof(MemberId))] public Member Member { get; set; } = null!;
	public ICollection<NfcScan> NfcScans { get; set; } = [];
	public ICollection<Transaction> Transactions { get; set; } = [];
}