using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync(string? brand, string? type, string? sort, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetProductBrandsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetProductTypesAsync(CancellationToken cancellationToken = default);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}
