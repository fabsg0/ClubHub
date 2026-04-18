using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClubHub.Domain.Enums;

namespace ClubHub.Domain.Entities;

[Table("online_orders")]
public class OnlineOrder
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	[Required] [MaxLength(100)] public string FirstName { get; set; } = string.Empty;

	[Required] [MaxLength(100)] public string LastName { get; set; } = string.Empty;

	[Required] [MaxLength(255)] public string Email { get; set; } = string.Empty;

	/// <summary>Amount paid in cents</summary>
	[Required]
	public int AmountPaid { get; set; }

	[Required] [MaxLength(20)] public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

	[MaxLength(255)] public string? StripeSessionId { get; set; }

	/// <summary>6-digit code for pickup at club office</summary>
	[MaxLength(20)]
	public string? PickupCode { get; set; }

	/// <summary>Set after pickup is confirmed</summary>
	public Guid? SeasonCardId { get; set; }

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public DateTime OrderedAt { get; set; }

	public DateTime? PickedUpAt { get; set; }

	[ForeignKey(nameof(SeasonCardId))] public SeasonCard? SeasonCard { get; set; }
}