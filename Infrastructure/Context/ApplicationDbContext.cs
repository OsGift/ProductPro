using Application.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
        _configuration = configuration;
    }
    #region Use InMemory
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "AuthServiceDb");
    }
    #endregion

    #region Using Sql Server
    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    //}
    #endregion


    public DbSet<Domain.Entities.Product> Products { get; set; } = default!; 
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}