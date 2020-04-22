using BLL.Repositories;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace WebApplication1612.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Everybody")]
    public class ProductsController : ControllerBase
    {
        private readonly UserManager<WebAppUser> _userManager;
  //      private IProductRepository _productRepository { get; set; }
        private IProductService _productService { get; set; } //integrating a Service, but using just a Repository is not a bad idea to me

        public ProductsController(UserManager<WebAppUser> userManager, IProductRepository productRepository, IProductService productService)
        {
            _userManager = userManager;
   //         _productRepository = productRepository;
            _productService = productService;
        }
        // GET: api/Products
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Product>>> GetProducts() //Instead of return the Entity(or list of) it should be returning a DTO objet (ViewModel)  in order to take care of sensitive data
        {
            return await _productService.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) //Instead of return the Entity(or list of) it should be returning a DTO objet (ViewModel)   in order to take care of sensitive data
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product) //Instead of use the Entity it should be using a DTO objet (ViewModel)   in order to take care of sensitive data
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                await _productService.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product) //Instead of use the Entity it should be using a DTO objet (ViewModel)   in order to take care of sensitive data
        {
            await _productService.CreateAsync(product); 

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(product);

            return product;
        }
    }
}
