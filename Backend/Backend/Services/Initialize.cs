
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Services
{
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

                context.AddRange(
                    new User
                    {
                        Name = "Администратор",
                        Email = "Admin@gmail.com",
                        Password = "123456",
                        RoleId = 1
                    }
                ); ;

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
                context.SaveChanges();

            }
        }
    }
}
