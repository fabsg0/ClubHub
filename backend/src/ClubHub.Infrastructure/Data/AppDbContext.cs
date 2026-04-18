using ClubHub.Application.Interfaces;
using ClubHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubHub.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
	public DbSet<Member> Members => Set<Member>();
	public DbSet<SeasonCard> SeasonCards => Set<SeasonCard>();
	public DbSet<NfcScan> NfcScans => Set<NfcScan>();
	public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Transaction> Transactions => Set<Transaction>();
	public DbSet<TransactionItem> TransactionItems => Set<TransactionItem>();
	public DbSet<OnlineOrder> OnlineOrders => Set<OnlineOrder>();
	public DbSet<AppUser> AppUsers => Set<AppUser>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Unique indexes (not expressible via attributes)
		modelBuilder.Entity<Member>()
			.HasIndex(x => x.Email).IsUnique();

		modelBuilder.Entity<SeasonCard>()
			.HasIndex(x => x.NfcKey).IsUnique();

		modelBuilder.Entity<OnlineOrder>()
			.HasIndex(x => x.StripeSessionId).IsUnique()
			.HasFilter("\"StripeSessionId\" IS NOT NULL");

		modelBuilder.Entity<OnlineOrder>()
			.HasIndex(x => x.PickupCode).IsUnique()
			.HasFilter("\"PickupCode\" IS NOT NULL");

		// Enum → string conversions
		modelBuilder.Entity<SeasonCard>()
			.Property(x => x.Status).HasConversion<string>();

		modelBuilder.Entity<NfcScan>()
			.Property(x => x.ScanType).HasConversion<string>();

		modelBuilder.Entity<OnlineOrder>()
			.Property(x => x.PaymentStatus).HasConversion<string>();

		modelBuilder.Entity<AppUser>()
			.Property(x => x.Role).HasConversion<string>();

		// Default values for timestamps
		modelBuilder.Entity<Member>()
			.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

		modelBuilder.Entity<SeasonCard>()
			.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

		modelBuilder.Entity<NfcScan>()
			.Property(x => x.ScannedAt).HasDefaultValueSql("now()");

		modelBuilder.Entity<Transaction>()
			.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

		modelBuilder.Entity<OnlineOrder>()
			.Property(x => x.OrderedAt).HasDefaultValueSql("now()");

		modelBuilder.Entity<AppUser>()
			.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

		// Check constraints
		modelBuilder.Entity<Product>()
			.ToTable(t => t.HasCheckConstraint("chk_price", "\"PriceCents\" >= 0"));

		modelBuilder.Entity<TransactionItem>()
			.ToTable(t => t.HasCheckConstraint("chk_qty", "\"Quantity\" > 0"));

		// Cascade behaviors for nullable FKs
		modelBuilder.Entity<NfcScan>()
			.HasOne(x => x.SeasonCard)
			.WithMany(c => c.NfcScans)
			.HasForeignKey(x => x.SeasonCardId)
			.OnDelete(DeleteBehavior.SetNull);

		modelBuilder.Entity<Transaction>()
			.HasOne(x => x.SeasonCard)
			.WithMany(c => c.Transactions)
			.HasForeignKey(x => x.SeasonCardId)
			.OnDelete(DeleteBehavior.SetNull);

		modelBuilder.Entity<TransactionItem>()
			.HasOne(x => x.Product)
			.WithMany(p => p.TransactionItems)
			.HasForeignKey(x => x.ProductId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OnlineOrder>()
			.HasOne(x => x.SeasonCard)
			.WithMany()
			.HasForeignKey(x => x.SeasonCardId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}