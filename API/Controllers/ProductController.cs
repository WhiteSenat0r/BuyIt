using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public int GetProducts() => 200;

    [HttpGet("{{productId}}")]
    public int GetProduct(Guid productGuid) => 200;
}