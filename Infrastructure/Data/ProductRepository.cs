using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(string? brand, string? type, string? sort, CancellationToken cancellationToken = default)
    {
        var query = context.Products.AsQueryable();

        if(!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand == brand);

        if(!string.IsNullOrEmpty(type))
            query = query.Where(p => p.Type == type);

        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetProductBrandsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Products.Select(p => p.Brand)
        .Distinct()
        .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Products.FindAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetProductTypesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Products.Select(p => p.Type)
        .Distinct()
        .ToListAsync(cancellationToken);
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
