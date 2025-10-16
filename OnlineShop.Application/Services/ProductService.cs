using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Application.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();

    }
}