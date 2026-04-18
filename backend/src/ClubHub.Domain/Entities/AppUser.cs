using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClubHub.Domain.Enums;

namespace ClubHub.Domain.Entities;

[Table("app_users")]
public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] [MaxLength(100)] public string Username { get; set; } = string.Empty;

    [Required] [MaxLength(255)] public string PasswordHash { get; set; } = string.Empty;

    [Required] [MaxLength(20)] public UserRole Role { get; set; } = UserRole.Cashier;

    [Required] public bool Active { get; set; } = true;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }
}