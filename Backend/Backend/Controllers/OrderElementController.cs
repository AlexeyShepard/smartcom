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
    public class OrderElementController : ControllerBase
    {
        private SmartcomContext DB;

        public OrderElementController(SmartcomContext Context)
        {
            DB = Context;
        }

        [HttpPost]
        public async Task<ActionResult<OrderElement>> Post(OrderElement OrderElement)
        {

            if (OrderElement == null) return BadRequest();
            try
            {
                DB.OrderElements.Add(OrderElement);
                await DB.SaveChangesAsync();

                return Ok(OrderElement);
            }
            catch (DbUpdateException Ex)
            {
                return Problem(Ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderElement>>> Get()
        {
            return await DB.OrderElements.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderElement>> Get(int id)
        {
            OrderElement OrderElement = await DB.OrderElements.FirstOrDefaultAsync(x => x.Id == id);
            if (OrderElement == null) return NotFound();

            return new ObjectResult(OrderElement);
        }

        [HttpPut]
        public async Task<ActionResult<OrderElement>> Put(OrderElement OrderElement)
        {
            if (OrderElement == null)
            {
                return BadRequest();
            }
            if (!DB.OrderElements.Any(x => x.Id == OrderElement.Id))
            {
                return NotFound();
            }

            DB.Update(OrderElement);
            await DB.SaveChangesAsync();
            return Ok(OrderElement);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderElement>> Delete(int id)
        {
            OrderElement OrderElement = DB.OrderElements.FirstOrDefault(x => x.Id == id);
            if (OrderElement == null)
            {
                return NotFound();
            }
            DB.OrderElements.Remove(OrderElement);
            await DB.SaveChangesAsync();
            return Ok(OrderElement);
        }
    }
}

