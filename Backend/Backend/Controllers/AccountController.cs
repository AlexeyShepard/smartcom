using Backend.Models;
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
using System.IdentityModel.Tokens.Jwt;
using Backend.Services;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ExtendController
    {
        private SmartcomContext DB;

        public AccountController(SmartcomContext Context)
        {
            DB = Context;
        }

        [HttpPost]
        public async Task<JsonResult> Register(RegisterModel Model)
        {
            if (ModelState.IsValid)
            {
                User User = await DB.Users.FirstOrDefaultAsync(u => u.Email == Model.Email);
                if (User == null)
                {
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

                    Authenticate(User);

                    return JsonResponse(new
                    {
                        Status = "Ok",
                        Token = AuthJwt(User),
                        Name = User.Name,
                        Email = User.Email,
                        RoleId = User.Role.Id
                    });
                }
                else
                {
                    ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                    return JsonResponse(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                return JsonResponse(ModelState);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<JsonResult> Login(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                User User = await DB.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == Model.Email && u.Password == Model.Password);

                if (User != null)
                {
                    var Response = new
                    {
                        Status = "Ок",
                        Token = AuthJwt(User),
                        Name = User.Name,
                        Email = User.Email,
                        RoleId = User.Role.Id

                    };

                    return JsonResponse(Response);
                }
                else
                {
                    ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                    return JsonResponse(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("error", "Некорректные логин и(или) пароль");
                return JsonResponse(ModelState);
            }
        }

        private string GenerateCode()
        {
            DateTime DateNow = DateTime.Now;
            Random Random = new Random();
            int Value = Random.Next(1000, 9999);


            return Value.ToString() + "-" + DateNow.ToString("yyyy");
        }        

        private string AuthJwt(User User)
        {
            var identity = Authenticate(User);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        
        private ClaimsIdentity Authenticate(User User)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, User.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role?.Name)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}

