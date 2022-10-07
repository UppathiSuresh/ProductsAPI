using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly PaymentDetailDBContext _context;
        private readonly IConfiguration _Configuration;

        public ProductsController(PaymentDetailDBContext context, IConfiguration configuration)
        {
            _context = context;
            _Configuration = configuration;
        }


        // Get: ProductsController/GetALL
        [HttpGet]
        public async Task<IEnumerable<Product>> GetALL()
        {
            Debug.WriteLine("value:{}"+ _Configuration["Test"]);
            return await _context.Products.ToListAsync();
        }

        // Get: ProductsController/Get/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();
            Console.WriteLine(Json(product));
            return Ok(product);

        }

        // POST: ProductsController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: ProductsController/Edit
        [HttpPut]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (product == null || id == 0)
                return BadRequest();

            var productData = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            var productData1 = await _context.Products.FindAsync(id);

            if (productData1 == null)
                return NotFound();

            productData1.Name = product.Name;
            productData1.Description = product.Description;
            productData1.Price = product.Price;

            await _context.SaveChangesAsync();

            return Ok(product);
        }


        // Delete: ProductsController/Delete/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id < 0)
                return BadRequest();
            var productData1 = await _context.Products.FindAsync(id);
            if (productData1 == null)
                return NotFound();

            _context.Products.Remove(productData1);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}




















//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ProductsAPI.Models;

//namespace ProductCRUDAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductsController : ControllerBase
//    {
//        private readonly PaymentDetailDBContext _context;

//        public ProductsController(PaymentDetailDBContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IEnumerable<Product>> Get()
//        {
//            return await _context.Products.ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(int id)
//        {
//            if (id < 1)
//                return BadRequest();
//            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
//            if (product == null)
//                return NotFound();
//            return Ok(product);

//        }

//        [HttpPost]
//        public async Task<IActionResult> Post(Product product)
//        {
//            _context.Add(product);
//            await _context.SaveChangesAsync();
//            return Ok();
//        }

//        [HttpPut]
//        public async Task<IActionResult> Put(Product productData)
//        {
//            if (productData == null || productData.Id == 0)
//                return BadRequest();

//            var product = await _context.Products.FindAsync(productData.Id);
//            if (product == null)
//                return NotFound();
//            product.Name = productData.Name;
//            product.Description = productData.Description;
//            product.Price = productData.Price;
//            await _context.SaveChangesAsync();
//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            if (id < 1)
//                return BadRequest();
//            var product = await _context.Products.FindAsync(id);
//            if (product == null)
//                return NotFound();
//            _context.Products.Remove(product);
//            await _context.SaveChangesAsync();
//            return Ok();

//        }
//    }
//}
