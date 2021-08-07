using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult<Order>> Post(List<int> ProductIds)
        {

            if(ProductIds.Count == 0) return BadRequest();
            //if (Order == null || !ModelState.IsValid) return BadRequest();
            try
            {
                Status OrderStatus = await DB.Statuses.FirstOrDefaultAsync(r => r.Name == "Новый");

                //int CustomerId = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier)).Value);
                string CustomerId = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

                Customer Customer = await DB.Customers.FirstOrDefaultAsync(c => c.UserId == Convert.ToInt32(CustomerId));


                Order Order = new Order
                {
                    Order_Date = DateTime.Now,
                    Order_Number = GenerateNumber(),
                    Customer = Customer,
                    Status = OrderStatus,                 
                };

                DB.Orders.Add(Order);
                await DB.SaveChangesAsync();

                ProductIds.Sort();
                int CashProductId = 0;

                foreach(int ProductId in ProductIds)
                {
                    if (CashProductId == ProductId) continue;
                    int ProductCount = 0;

                    foreach(int CProductId in ProductIds) if (CProductId == ProductId) ProductCount++;
                    
                    Product Product = await DB.Products.FirstOrDefaultAsync(p => p.Id == ProductId);

                    OrderElement OrderElement = new OrderElement
                    {
                        Order = Order,
                        Product = Product,
                        Items_Count = ProductCount,
                        Item_Price = Product.Price * ProductCount
                    };

                    DB.OrderElements.Add(OrderElement);

                    CashProductId = ProductId;
                }

                await DB.SaveChangesAsync();

                return Ok();
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

        private int GenerateNumber()
        {
            Random Random = new Random();
            int Value = Random.Next(1000, 9999);

            return Value;

            /*DateTime DateNow = DateTime.Now;

            return DateNow.ToString("yy-ffff-ttdd");*/
        }
    }
}
