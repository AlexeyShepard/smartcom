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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private SmartcomContext DB;

        public OrderController(SmartcomContext Context)
        {
            DB = Context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(Order Order)
        {

            if (Order == null || !ModelState.IsValid) return BadRequest();
            try
            {
                DB.Orders.Add(Order);
                await DB.SaveChangesAsync();

                return Ok(Order);
            }
            catch (DbUpdateException Ex)
            {
                return Problem(Ex.InnerException.Message);
            }
        }

        [Authorize(Roles = "Менеджер")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await DB.Orders.ToListAsync();
        }

        [Authorize(Roles = "Менеджер")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order Order = await DB.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (Order == null) return NotFound();

            return new ObjectResult(Order);
        }

        [Authorize(Roles = "Менеджер")]
        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order Order)
        {
            if (Order == null || ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!DB.Users.Any(x => x.Id == Order.Id))
            {
                return NotFound();
            }

            DB.Update(Order);
            await DB.SaveChangesAsync();
            return Ok(Order);
        }

        [Authorize(Roles = "Менеджер")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            Order Order = DB.Orders.FirstOrDefault(x => x.Id == id);
            if (Order == null)
            {
                return NotFound();
            }
            DB.Orders.Remove(Order);
            await DB.SaveChangesAsync();
            return Ok(Order);
        }
    }
}
