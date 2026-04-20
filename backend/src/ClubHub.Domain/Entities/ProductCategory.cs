using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubHub.Domain.Entities;

[Table("product_categories")]
public class ProductCategory
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	[Required][MaxLength(100)] public string Name { get; set; } = string.Empty;

	[MaxLength(255)] public string? Description { get; set; }

	public int SortOrder { get; set; }

	public ICollection<Product> Products { get; set; } = [];
}
