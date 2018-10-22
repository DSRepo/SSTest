using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SSTest.Core.DataAccess;
using SSTest.Core.Interface;
using SSTest.Core.Utilies;
using NLog;

namespace SSTest.Service.Controllers
{
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Logger Implementation
        /// </summary>
        private static readonly ILogger _logger = LogManager.GetLogger("PoductAPILog");
        /// <summary>
        /// IOC implementation. This can be tested using any Testing/Mocking framework
        /// </summary>
        private IProductRepository _iProductRepository;       
        public ProductsController(IProductRepository iProductRepository)
        {
            _logger.Info("API Executed");
            _iProductRepository = iProductRepository;
        }

        // GET: api/Products
        /// <summary>
        /// /Get List of all the Products
        /// </summary>
        /// <returns>list of products </returns>
        [Route("api/products")]
        [HttpGet]
        [ResponseType(typeof(IList<Product>))]
        public IList<Product> GetProducts()
        {
            try
            {
                return _iProductRepository.GetAll();
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }

        // GET: api/Products/5
        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        [Route("api/products/{id:int}")]
        [HttpGet]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await _iProductRepository.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        /// <summary>
        /// Update Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [Route("api/products/{id:int}")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

         
            try
            {
                product.LastUpdated = System.DateTime.Now;
                await _iProductRepository.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.Error(ex);
                    return Content(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        /// <summary>
        /// Add New Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Route("api/products")]
        [HttpPost]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                product.LastUpdated = System.DateTime.Now;
                await _iProductRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }


            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/products/{id:int}")]
        [HttpPost]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await _iProductRepository.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }


            await _iProductRepository.DeleteAsync(id);

            return Ok(product);
        }

        /// <summary>
        /// Dispose the Objects
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _iProductRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Check if Product exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ProductExists(int id)
        {
            return  _iProductRepository.GetAll().Count(e => e.Id == id) > 0;
        }
    }
}