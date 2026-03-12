using ASP_NET_03._Dependency_Injection___services.Models;
using ASP_NET_03._Dependency_Injection___services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_03._Dependency_Injection___services.Pages;

public class IndexModel : PageModel
{
    private ProductService _service;

    public IndexModel(ProductService service)
    {
        _service = service;
    }

    public void OnGet()
    {
        var products = _service.GetProducts();
        ViewData["Products"] = products;
    }
}
