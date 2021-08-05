using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize(Roles="Менеджер")]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private SmartcomContext DB;

        public ProductController(SmartcomContext Context)
        {
            DB = Context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product Product)
        {

            if (Product == null) return BadRequest();
            try
            {
                DB.Products.Add(Product);
                await DB.SaveChangesAsync();

                return Ok(Product);
            }
            catch (DbUpdateException Ex)
            {
                return Problem(Ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await DB.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Product Product = await DB.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (Product == null) return NotFound();

            return new ObjectResult(Product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product Product)
        {
            if (Product == null)
            {
                return BadRequest();
            }
            if (!DB.Products.Any(x => x.Id == Product.Id))
            {
                return NotFound();
            }

            DB.Update(Product);
            await DB.SaveChangesAsync();
            return Ok(Product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            Product Product = DB.Products.FirstOrDefault(x => x.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            DB.Products.Remove(Product);
            await DB.SaveChangesAsync();
            return Ok(Product);
        }
    }
}
