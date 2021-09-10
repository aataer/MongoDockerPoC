using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region DI
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        #endregion

        #region Constructor
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        #endregion

        #region Crud_Actions
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }


        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id:{id},couldnot found in db");
                return NotFound();
            }
            return Ok(product);
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        //{
        //    var product = await _productRepository.GetProductByName(name);

        //    return Ok(product);
        //}


        //[HttpGet]
        //[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        //{
        //    var product = await _productRepository.GetProductByCategory(category);

        //    return Ok(product);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<IEnumerable<bool>>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);


            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<bool>>> UpdateProduct([FromBody] Product product)
        {
            var result = await _productRepository.Update(product);

            return Ok(result);
        }
        [HttpPost("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<bool>>> DeleteProductById(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }
        #endregion
    }
}
