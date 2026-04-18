using ClubHub.Domain.Enums;

namespace ClubHub.Application.DTOs.SeasonCards;

public class SeasonCardBaseDto
{
    public Guid Id { get; set; }
    public string NfcKey { get; set; } = string.Empty;

    /// <summary> Paid in cents </summary>
    public int AmountPaid { get; set; }

    public int FreeDrinksTotal { get; set; }
    public int FreeDrinksUsed { get; set; }
    public DateOnly ValidFrom { get; set; }
    public DateOnly ValidUntil { get; set; }
    public CardStatus Status { get; set; } = CardStatus.Active;
}