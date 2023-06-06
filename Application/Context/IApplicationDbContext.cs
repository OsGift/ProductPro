using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Context;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}