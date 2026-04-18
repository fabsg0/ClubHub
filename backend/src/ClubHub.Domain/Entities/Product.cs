using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubHub.Domain.Entities;

[Table("products")]
public class Product
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	[Required] public Guid ProductCategoryId { get; set; }

	[Required] [MaxLength(150)] public string Name { get; set; } = string.Empty;

	/// <summary>Price in cents, e.g. 250 = €2.50</summary>
	[Required]
	public int PriceCents { get; set; }

	/// <summary>Relevant for free drink redemption logic</summary>
	[Required]
	public bool IsDrink { get; set; }

	[Required] public bool Available { get; set; } = true;

	public int SortOrder { get; set; }

	[ForeignKey(nameof(ProductCategoryId))]
	public ProductCategory ProductCategory { get; set; } = null!;

	public ICollection<TransactionItem> TransactionItems { get; set; } = [];
}