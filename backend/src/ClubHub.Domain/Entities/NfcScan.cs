using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClubHub.Domain.Enums;

namespace ClubHub.Domain.Entities;

[Table("nfc_scans")]
public class NfcScan
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	public Guid? SeasonCardId { get; set; }

	[Required][MaxLength(100)] public string RawNfcKey { get; set; } = string.Empty;

	[Required][MaxLength(20)] public ScanType ScanType { get; set; }

	[Required] public bool Granted { get; set; }

	/// <summary>e.g. "expired", "blocked", "not_found"</summary>
	[MaxLength(100)]
	public string? DenyReason { get; set; }

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public DateTime ScannedAt { get; set; }

	[ForeignKey(nameof(SeasonCardId))] public SeasonCard? SeasonCard { get; set; }
}
