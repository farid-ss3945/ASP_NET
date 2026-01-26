using ASP_NET_03._Dependency_Injection___services.Models;

namespace ASP_NET_03._Dependency_Injection___services.Data;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public IEnumerable<Product> GetProducts();
}
