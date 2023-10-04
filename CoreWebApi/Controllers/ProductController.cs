using CoreWebApi.DB;
using CoreWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductController(AppDbContext appDbContext) 
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return _dbContext.Products;
        }


        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> Details(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
