using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubHub.Domain.Entities;

[Table("transaction_items")]
public class TransactionItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] public Guid TransactionId { get; set; }

    [Required] public Guid ProductId { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Quantity { get; set; } = 1;

    /// <summary>Price snapshot at time of purchase</summary>
    [Required]
    public int UnitPriceCents { get; set; }

    [Required] public bool WasFree { get; set; }

    [ForeignKey(nameof(TransactionId))] public Transaction Transaction { get; set; } = null!;

    [ForeignKey(nameof(ProductId))] public Product Product { get; set; } = null!;
}