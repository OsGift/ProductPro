using Application.Context;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Context;

public class ApplicationSeed : IApplicationSeed
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ApplicationSeed> _logger;

    public ApplicationSeed(IApplicationDbContext context, ILogger<ApplicationSeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async void SeedData()
    {
        try
        {
            _logger.LogInformation("About to start data seeding");

            // Check if the data has already been seeded
            if (_context.Products.Any() && _context.Products.Any())
            {
                Console.WriteLine("Data has already been seeded. Skipping data seeding process.");
                return;
            }
            // Perform your data seeding logic here
            SeedProducts();
            await _context.SaveChangesAsync(new CancellationToken());
            _logger.LogInformation("Data seeding completed successfully.");
        }
        catch (Exception ex)
        {
            // Log and handle the exception
            _logger.LogError($"Error seeding data: {ex.Message}");
        }
    }

    private void SeedProducts()
    {

        // Create the admin user
        var products = new List<Product>
        {
           new Product
           {
               Price = 100,
               ProductName="Bag",
               Qty=20
           },
           new Product
           {
               Price = 200,
               ProductName="Shoe",
               Qty=30
           },
           new Product
           {
               Price = 500,
               ProductName="Phone",
               Qty=130
           }
        };
        _context.Products.AddRange(products);
    }
}