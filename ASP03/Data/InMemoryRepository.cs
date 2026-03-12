using ASP_NET_03._Dependency_Injection___services.Models;
namespace ASP_NET_03._Dependency_Injection___services.Data;

public class InMemoryRepository : IProductRepository
{
    private readonly IDictionary<Guid, Product> _products
        = new Dictionary<Guid, Product>();

    public InMemoryRepository()
    {
        AddProduct(new Product { Name = "Milk", Description = "3% Natural Milk" });
        AddProduct(new Product { Name = "Milka", Description = "Milka wokolad" });
        AddProduct(new Product { Name = "Silk", Description = "China quality silk" });
        AddProduct(new Product { Name = "Egg", Description = "China quality fake egg" });
        AddProduct(new Product { Name = "Fil", Description = "African elephant" });
        AddProduct(new Product { Name = "Bread", Description = "Chinees Bread Pitt" });
    }
    public Product AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        _products.Add(product.Id, product);
        return product;
    }
    public IEnumerable<Product> GetProducts()
    {
        return _products.Values;
    }
}
