using Application.Context;
using Domain.Entities;

namespace Application.Products
{
    public class CreateProduct
    {
        private readonly IApplicationDbContext _context;

        public CreateProduct(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync(new CancellationToken());
            }
            catch (Exception ex)
            {
            }
            return product.Id;
        }
    }
}
