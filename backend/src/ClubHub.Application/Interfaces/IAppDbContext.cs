using ClubHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubHub.Application.Interfaces;

public interface IAppDbContext
{
	DbSet<Member> Members { get; }
	DbSet<SeasonCard> SeasonCards { get; }
	DbSet<NfcScan> NfcScans { get; }
	DbSet<ProductCategory> ProductCategories { get; }
	DbSet<Product> Products { get; }
	DbSet<Transaction> Transactions { get; }
	DbSet<TransactionItem> TransactionItems { get; }
	DbSet<OnlineOrder> OnlineOrders { get; }
	DbSet<AppUser> AppUsers { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}