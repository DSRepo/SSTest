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

namespace SSTest.Service.Controllers
{
    public class ProductsController : ApiController
    {
        
        private IProductRepository _iProductRepository;
       
        public ProductsController(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }

        // GET: api/Products
        /// <summary>
        /// /Get List of all the Products
        /// </summary>
        /// <returns>list of products </returns>
        public IList<Product> GetProducts()
        {
            return _iProductRepository.GetAll();
        }

        // GET: api/Products/5
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

           // product = await _iProductRepository.FindAsync(id);
            //db.Entry(product).State = EntityState.Modified;

            try
            {
                product.LastUpdated = System.DateTime.Now;
                await _iProductRepository.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
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
                throw ex;
            }


            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _iProductRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return  _iProductRepository.GetAll().Count(e => e.Id == id) > 0;
        }
    }
}