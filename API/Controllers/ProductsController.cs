using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;


        public ProductsController(StoreContext context)
        {
            // jpa - this is constructor dependency injection.
            // When a request comes into our controller, our framework is going to route our
            // request to our ProductsController and instatiates a new instance of our 
            // productscontroller. From there it takes a look inside our constructor and it says 
            // aha I see you need a service, in this case the StoreContext and because our service 
            // is registered in our program class, program.cs, AddDbContext then it's going
            // to use this service and class (DbContext) and effectively create a new instance of 
            // our DbContext that our ProductsController can then use.
            // And now we have access to all the DbContext methods inside our controller.

            // When our request is finished our  productsController is disposed of along with 
            // the DbContext StoreContext service, so as developers we dont need to worry
            // about memory management and disposing of objects for the mostpart when using
            // dependency injection, the framework will do this for use ... with a few exceptions.

            _context = context;
        }

        // jpa - Async is like a background process in unix.
        // it puts th current request on the stack and then puts the next request on the stack
        // notifying the appropriate request on completion and only uses one thread. Makes a big
        // difference with scalability. Current best practice.
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}