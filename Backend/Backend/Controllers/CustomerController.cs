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
    [Authorize(Roles = "Менеджер")]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private SmartcomContext DB;

        public CustomerController(SmartcomContext Context)
        {
            DB = Context;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer Customer)
        {

            if (Customer == null) return BadRequest();
            try
            {
                DB.Customers.Add(Customer);
                await DB.SaveChangesAsync();

                return Ok(Customer);
            }
            catch (DbUpdateException Ex)
            {
                return Problem(Ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return await DB.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            Customer Customer = await DB.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (Customer == null) return NotFound();

            return new ObjectResult(Customer);
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> Put(Customer Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }
            if (!DB.Customers.Any(x => x.Id == Customer.Id))
            {
                return NotFound();
            }

            DB.Update(Customer);
            await DB.SaveChangesAsync();
            return Ok(Customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            Customer Customer = DB.Customers.FirstOrDefault(x => x.Id == id);
            if (Customer == null)
            {
                return NotFound();
            }
            DB.Customers.Remove(Customer);
            await DB.SaveChangesAsync();
            return Ok(Customer);
        }
    }
}
