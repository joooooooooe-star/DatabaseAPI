using DatabaseAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductNameShapeController : ControllerBase
    { 
        private readonly DataContext _context;
        public ProductNameShapeController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IDictionary<string, List<string>>>> Get()
        {
            List<Product> products = await _context.Products.ToListAsync();
            Dictionary<string, List<string>> ret = new();
            foreach (Product product in products)
            {
                if (ret.ContainsKey(product.ProductName))
                {
                    ret[product.ProductName].Add(product.ProductShape);
                }
                else
                {
                    ret.Add(product.ProductName, new List<string> { product.ProductShape });   
                }
            }
            return Ok(ret);
        }
    }
}
