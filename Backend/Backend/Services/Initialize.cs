
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Services
{
    /// <summary>
    /// Инициализация данных для БД
    /// </summary>
    public class Initialize
    {
        public static void Run(SmartcomContext context)
        {
            if (!context.Roles.Any() && !context.Statuses.Any() && !context.Categories.Any())
            {
                
                context.AddRange(
                    new Role
                    {
                        Name = "Менеджер"
                    },
                    new Role
                    {
                        Name = "Заказчик"
                    });

                User User = new User
                {
                    Name = "Заказчик Евгений",
                    Email = "Customer@gmail.com",
                    Password = "123456",
                    RoleId = 2
                };


                context.AddRange(
                    new User
                    {
                        Name = "Администратор",
                        Email = "Admin@gmail.com",
                        Password = "123456",
                        RoleId = 1
                    });

                context.AddRange(
                    new Customer
                    {
                        User = User,
                        Adress = "г. Москва",
                        Code = "1111-1111"                        
                    });

                context.AddRange(
                    new Status
                    {
                        Name = "Новый"
                    },
                    new Status
                    {
                        Name = "Выполняется"
                    },
                    new Status
                    {
                        Name = "Выполнен"
                    });
                context.AddRange(
                    new Category
                    {
                        Name = "Молочные"
                    },
                    new Category
                    {
                        Name = "Мясо"
                    },
                    new Category
                    {
                        Name = "Крупы"
                    },
                    new Category
                    {
                        Name = "Выпечка"
                    });

                context.AddRange(
                    new Product
                    {
                        Name = "Молоко",
                        Price = 100,
                        Code = "11-1111-AS11",
                        CategoryId = 1
                    });
                context.SaveChanges();

            }
        }
    }
}
