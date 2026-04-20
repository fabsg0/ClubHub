using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubHub.Domain.Entities;

[Table("transactions")]
public class Transaction
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	/// <summary>Nullable — not every purchase uses a season card</summary>
	public Guid? SeasonCardId { get; set; }

	[Required] public int TotalCents { get; set; }

	[Required] public int FreeDrinksRedeemed { get; set; }

	[MaxLength(500)] public string? Note { get; set; }

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public DateTime CreatedAt { get; set; }

	[ForeignKey(nameof(SeasonCardId))] public SeasonCard? SeasonCard { get; set; }
	public ICollection<TransactionItem> Items { get; set; } = [];
}
