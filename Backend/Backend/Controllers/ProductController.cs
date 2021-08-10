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
    public class FormatedCategory
    {
        public string text { get; set; }

        public int value { get; set; }
    }
    
    
    [Authorize]
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
                Product.Code = GenerateCode();
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
        public async Task<ActionResult<IEnumerable<CRUDProduct>>> Get()
        {

            //return await DB.Products.ToListAsync();

            List<CRUDProduct> ProductsCategory = new List<CRUDProduct>();

            foreach (Product Product in DB.Products.ToList())
            {
                Category Category = await DB.Categories.FirstOrDefaultAsync(u => u.Id == Product.CategoryId);
                ProductsCategory.Add(new CRUDProduct
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Price = Product.Price,
                    Code = Product.Code,
                    CategoryName = Category.Name                 
                });
            }

            return ProductsCategory;
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

        [HttpGet]
        [Route("category")]
        public async Task<ActionResult<IEnumerable<FormatedCategory>>> GetCategory()
        {
            List<FormatedCategory> FCategories = new List<FormatedCategory>();
            
            foreach (Category Category in await DB.Categories.ToListAsync())
            {
                FCategories.Add(new FormatedCategory
                {
                    text = Category.Name,
                    value = Category.Id
                });               
            }

            return FCategories;
        }

        private string GenerateCode()
        {
            DateTime DateNow = DateTime.Now;

            return DateNow.ToString("yy-ffff-") + "AA" + DateNow.ToString("dd");
        }
    }
}
