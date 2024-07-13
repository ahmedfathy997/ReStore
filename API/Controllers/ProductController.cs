using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
        // GET: api/Products/Id
        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            return Ok(product);
        }

    }
}
