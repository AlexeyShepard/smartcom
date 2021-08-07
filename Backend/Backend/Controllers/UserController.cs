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
    public class UserController : ControllerBase
    {
        private SmartcomContext DB;

        public UserController(SmartcomContext Context)
        {
            DB = Context;
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> Post(User User)
        {
            
            if (User == null) return BadRequest();
            try
            {
                DB.Users.Add(User);

                await DB.SaveChangesAsync();

                return Ok(User);
            }
            catch(DbUpdateException Ex)
            {
                return Problem(Ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await DB.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await DB.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();

            return new ObjectResult(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!DB.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            DB.Update(user);
            await DB.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = DB.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            DB.Users.Remove(user);
            await DB.SaveChangesAsync();
            return Ok(user);
        }
    }
}
