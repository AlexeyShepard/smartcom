﻿using Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Web;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Amazon.CloudFront.Model;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SmartcomContext DB;

        public AccountController(SmartcomContext Context)
        {
            DB = Context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel Model)
        {
            if (ModelState.IsValid)
            {
                User User = await DB.Users.FirstOrDefaultAsync(u => u.Email == Model.Email);
                if (User == null)
                {
                    // добавляем пользователя в бд
                    User = new User { Name = Model.Name, Email = Model.Email, Password = Model.Password };
                    Role UserRole = await DB.Roles.FirstOrDefaultAsync(r => r.Name == "Заказчик");
                    if (UserRole != null) User.Role = UserRole;

                    Customer UserCustomer = new Customer
                    {
                        Adress = Model.Adress,
                        User = User,
                        Code = GenerateCode()
                    };

                    DB.Users.Add(User);
                    DB.Customers.Add(UserCustomer);
                    await DB.SaveChangesAsync();

                    await Authenticate(User); // аутентификация

                    return Ok(new
                    {
                        Name = Model.Name,
                        Email = Model.Email
                    });
                }
                else
                {
                    ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                    return BadRequest();
                }
            }
            else
            {
                ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                User User = await DB.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == Model.Email && u.Password == Model.Password);
                if (User != null)
                {
                    await Authenticate(User); // аутентификация

                    return Ok(User.Name);
                }
                else
                {
                    ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                    return BadRequest(ModelState);
                }            
            }
            else
            {
                ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            
            return Ok(new
            {
                Message = "Вы вышли из системы"
            });
        }

        private async Task Authenticate(User User)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, User.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
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
