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
        public async Task<ActionResult<Customer>> Post(CRUDCustomer CRUDCustomer)
        {

            if (CRUDCustomer == null) return BadRequest();
            try
            {
                
                User User = new User
                {
                    Name = CRUDCustomer.Name,
                    Email = CRUDCustomer.Email,
                    Password = CRUDCustomer.Password,
                };

                Role UserRole = await DB.Roles.FirstOrDefaultAsync(r => r.Name == "Заказчик");
                if (UserRole != null) User.Role = UserRole;

                Customer Customer = new Customer
                {
                    Code = GenerateCode(),
                    Adress = CRUDCustomer.Adress,
                    Discount = CRUDCustomer.Discount,
                    User = User
                };

                DB.Users.Add(User);

                DB.Customers.Add(Customer);
                await DB.SaveChangesAsync();

                return Ok("Заказчик добавлен");
            }
            catch (DbUpdateException Ex)
            {
                return Problem(Ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CRUDCustomer>>> Get()
        {
            List<CRUDCustomer> UserCustomers = new List<CRUDCustomer>();

            foreach(Customer Customer in DB.Customers.ToList()) {
                User User = await DB.Users.FirstOrDefaultAsync(u => u.Id == Customer.UserId);
                UserCustomers.Add(new CRUDCustomer
                    {
                        Id = Customer.Id,
                        Name = User.Name,
                        Email = User.Email,
                        Password = User.Password,
                        Code = Customer.Code,
                        Adress = Customer.Adress,
                        Discount = Customer.Discount,
                        UserId = Customer.UserId
                    }
                );
            }

            return UserCustomers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CRUDCustomer>> Get(int id)
        {
            Customer Customer = await DB.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (Customer == null) return NotFound();

            User User = await DB.Users.FirstOrDefaultAsync(u => u.Id == Customer.UserId);

            CRUDCustomer UserCustomer = new CRUDCustomer
            {
                Id = Customer.Id,
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                Code = Customer.Code,
                Adress = Customer.Adress,
                Discount = Customer.Discount,
                UserId = Customer.UserId
            };

            return new ObjectResult(UserCustomer);
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> Put(CRUDCustomer CRUDCustomer)
        {
            if (CRUDCustomer == null) return BadRequest();

            Customer Customer = new Customer
            {
                Id = CRUDCustomer.Id,
                Discount = CRUDCustomer.Discount,
                Adress = CRUDCustomer.Adress,
                Code = CRUDCustomer.Code,
                UserId = CRUDCustomer.UserId
            };

            if (!DB.Customers.Any(x => x.Id == Customer.Id)) return NotFound();

            User User = new User
            {
                Id = CRUDCustomer.UserId,
                Name = CRUDCustomer.Name,
                Email = CRUDCustomer.Email,
                Password = CRUDCustomer.Password,
                RoleId = 2
            };

            if (!DB.Users.Any(x => x.Id == User.Id)) return NotFound();

            DB.Update(Customer);
            DB.Update(User);
            await DB.SaveChangesAsync();
            return Ok("Пользователь изменён");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            Customer Customer = DB.Customers.FirstOrDefault(x => x.Id == id);
            if (Customer == null) return NotFound();
            User User = await DB.Users.FirstOrDefaultAsync(u => u.Id == Customer.UserId);
          
            DB.Customers.Remove(Customer);
            DB.Users.Remove(User);
            await DB.SaveChangesAsync();
            return Ok(Customer);
        }

        private string GenerateCode()
        {
            DateTime DateNow = DateTime.Now;
            Random Random = new Random();
            int Value = Random.Next(1000, 9999);


            return Value.ToString() + "-" + DateNow.ToString("yyyy");
        }
    }
}
